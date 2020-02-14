using DonVo.MongoDb.Console2018.ForGridFS.Attributes;
using DonVo.MongoDb.Console2018.ForGridFS.Mongo;

using System;

namespace DonVo.MongoDb.Console2018.ForGridFS.ViewModels
{
    public class ApplicationUser : DBObject<ApplicationUser>
    {
        public string Name { get; set; }

        public string MiddleName { get; set; }

        [FullTextIndex]
        public string FamilyName { get; set; }

        [AscendingIndex]
        public string UserName { get; set; }

        public int LogInNumber { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
