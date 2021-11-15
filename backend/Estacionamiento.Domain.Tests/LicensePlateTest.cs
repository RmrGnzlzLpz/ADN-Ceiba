using Estacionamiento.Domain.Attributes;
using Estacionamiento.Domain.Entities;
using Estacionamiento.Domain.Enums;
using Estacionamiento.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Estacionamiento.Domain.Tests
{
    [TestClass]
    public class LicensePlateTest
    {
        private LicensePlateAttribute licensePlateAttribute;

        [TestInitialize]
        public void Setup()
        {
            licensePlateAttribute = new LicensePlateAttribute();
        }

        [TestMethod]
        [DataRow("AAA123")]
        [DataRow("AA1234")]
        [DataRow("R12345")]
        [DataRow("T1234")]
        [DataRow("AAA12A")]
        [DataRow("CBD123")]
        public void ValidateLicensePlate_AllCases_Success(string license)
        {
            var licenseIsvalid = licensePlateAttribute.IsValid(license);

            Assert.IsTrue(licenseIsvalid, "Invalid license plate");
        }

        [TestMethod]
        [DataRow("123456")]
        [DataRow("12345")]
        [DataRow("ABCDEF")]
        [DataRow("ABCDE")]
        public void ValidateLicensePlate_OnlyNumbers_And_OnlyLetters_Fail(string license)
        {
            var licenseIsvalid = licensePlateAttribute.IsValid(license);

            Assert.IsFalse(licenseIsvalid, "Invalid license plate");
        }
    }
}
