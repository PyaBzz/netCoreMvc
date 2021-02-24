using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using myCoreMvc.Domain;
using Baz.Core;

namespace myCoreMvc.App.Services
{
    public class DataRepoMock : IDataRepo
    {
        private Config _Config;
        private List<WorkPlan> WorkPlans;
        private List<WorkItem> WorkItems;
        private List<User> Users;

        public DataRepoMock(Config conf)
        {
            this._Config = conf;
            WorkPlans = ReadFromXml<WorkPlan>();
            WorkItems = ReadFromXml<WorkItem>();

            foreach (var workItem in WorkItems)
                workItem.WorkPlan = Get<WorkPlan>(workItem.WorkPlanId);

            using (var rng = RandomNumberGenerator.Create())
            {
                var Salts = new[] { new byte[128 / 8], new byte[128 / 8], new byte[128 / 8] };
                foreach (var salt in Salts)
                    rng.GetBytes(salt);

                Users = new List<User>()
                {
                    new User {Id = Guid.Parse("5d45a66d-fc2d-4a7f-b9dc-aac9f723f034"), Salt = Salts[0], Name = "Jim",
                        Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("jjj", Salts[0], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                        DateOfBirth = new DateTime(2018, 01, 22), Role = AuthConstants.JuniorRoleName },
                    new User {Id = Guid.Parse("91555540-6137-4668-9d55-5c22471237f3"), Salt = Salts[1], Name = "Sam",
                        Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("sss", Salts[1], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                        DateOfBirth = new DateTime(2010, 01, 22), Role = AuthConstants.SeniorRoleName },
                    new User {Id = Guid.Parse("97ba3d59-a990-4b55-ba91-7865fca0a4a2"), Salt = Salts[2], Name = "Adam",
                        Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2("aaa", Salts[2], KeyDerivationPrf.HMACSHA512, 100, 256 / 8)),
                        DateOfBirth = new DateTime(2000, 01, 22), Role = AuthConstants.AdminRoleName }
                };
            }
        }

        private List<T> ReadFromXml<T>() where T : class
        {
            var res = new List<T>();
            var outputDir = Assembly.GetExecutingAssembly().GetDirectory();
            var typeName = typeof(T).Name;
            try
            {
                var sourceFilePath = Path.Combine(outputDir, _Config.Data.Path.XmlSource[typeName + "s"]);
                var serialiser = new XmlSerializer(typeof(T));

                using (var stream = File.OpenRead(sourceFilePath))
                {
                    using (var xmlRdr = XmlReader.Create(stream))
                    {
                        while (xmlRdr.Read())
                        {
                            if (xmlRdr.NodeType == XmlNodeType.Element && xmlRdr.Name == typeName)
                            {
                                var workPlan = serialiser.Deserialize(xmlRdr) as T;
                                res.Add(workPlan);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }

        private List<T> GetField<T>() where T : class, IThing
        {
            var fieldInfos = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
            var fieldInfo = fieldInfos.SingleOrDefault(pi => pi.FieldType == typeof(List<T>));
            if (fieldInfo == null)
                throw new NullReferenceException($"{this.GetType()} knows no source collection of type {typeof(T)}.");
            return (List<T>)fieldInfo.GetValue(this);
        }

        /*==================================  Interface Methods =================================*/

        public List<T> GetList<T>(Predicate<T> predicate = null) where T : class, IThing
        {
            if (predicate == null)
                return GetField<T>();
            else
                return GetField<T>().Where(x => predicate(x)).ToList();
        }

        public T Get<T>(Predicate<T> func) where T : class, IThing
            => GetField<T>().SingleOrDefault(i => func(i));

        public T Get<T>(Guid id) where T : class, IThing
            => GetField<T>().SingleOrDefault(i => i.Id == id);

        public TransactionResult Save<T>(T obj) where T : class, IThing
        {
            //Task: Is this the best way to determine if the object is new?
            if (obj.Id == Guid.Empty)
                return Add<T>(obj);
            else
                return Update<T>(obj);
        }

        private TransactionResult Add<T>(T obj) where T : class, IThing
        {
            obj.Id = Guid.NewGuid();
            var targetSource = GetField<T>();
            targetSource.Add(obj);
            return TransactionResult.Added;
        }

        private TransactionResult Update<T>(T obj) where T : class, IThing
        {
            var targetSource = GetField<T>();//Task: Use filtering query instead of LINQ!
            var existingObj = targetSource.SingleOrDefault(e => e.Id == obj.Id);
            if (existingObj == null)
                return TransactionResult.NotFound;
            else
            {
                existingObj.CopyPropertiesFrom(obj);
                return TransactionResult.Updated;
            }
        }

        public TransactionResult Delete<T>(Guid id) where T : class, IThing
        {
            var targetSource = GetField<T>();
            var existingObj = targetSource.SingleOrDefault(e => e.Id == id);
            if (existingObj == null) return TransactionResult.NotFound;
            targetSource.Remove(existingObj);
            return TransactionResult.Deleted;
        }

        // These are meant to determine depth of eager loading in an ORM
        public List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IThing
            => GetField<T>();

        // These are meant to determine depth of eager loading in an ORM
        public List<T> GetListIncluding<T>(Predicate<T> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IThing
            => GetList(predicate);
    }
}
