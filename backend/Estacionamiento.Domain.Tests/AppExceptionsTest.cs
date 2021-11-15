using Estacionamiento.Domain.Entities;
using Estacionamiento.Domain.Enums;
using Estacionamiento.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Estacionamiento.Domain.Tests
{
    [TestClass]
    public class AppExceptionsTest
    {
        [TestMethod]
        public void ThrowAlreadyParkedException_WithMessage_Success()
        {
            var message = "already park exception:custom";
            try
            {
                throw new AlreadyParkedException(message);
            } catch(AlreadyParkedException e)
            {
                Assert.AreEqual(message, e.Message);
            }
        }

        [TestMethod]
        public void ThrowAlreadyParkedException_WithoutMessage_Success()
        {
            const string defaultMessage = "Vehicle already parked";
            try
            {
                throw new AlreadyParkedException();
            }
            catch (AlreadyParkedException e)
            {
                Assert.AreEqual(defaultMessage, e.Message);
            }
        }
    }
}
