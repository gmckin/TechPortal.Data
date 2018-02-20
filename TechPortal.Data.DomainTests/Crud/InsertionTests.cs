﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechPortal.Data.Domain.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechPortal.Data.Domain.DataAccessObjects;

namespace TechPortal.Data.Domain.Crud.Tests
{
    [TestClass()]
    public partial class AccessHelperTests
    {

        //These are INETGRATION TESTS. DO NOT MISTAKE THESE FOR UNIT TESTS!!!

        [TestMethod()]
        public void InsertJobTitleTest()
        {
            AccessHelper ah = new AccessHelper();
            var title = new JobTitleDAO() { JobTitleName = "Uber Assistant", IsActive = true};
            var toInsert = ah.InsertJobTitle(title);

            Assert.IsTrue(toInsert);
        }

       
    }
}