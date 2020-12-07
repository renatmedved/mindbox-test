using MindboxTest.Contracts.Results;

using NUnit.Framework;

using System;

namespace MindboxTest.Contracts.Tests
{
    public class ResultTests
    {
        [Test]
        public void MakSuccess_SuccessFilled()
        {
            var result = Result<int>.MakeSucces(1);

            Assert.AreEqual(result.Data, 1);
        }

        [Test]
        public void MakeNullSuccess_DataNull()
        {
            var result = Result<object>.MakeSucces(null);

            Assert.AreEqual(result.Data, null);
        }

        [Test]
        public void MakeSuccess_SuccessTrue()
        {
            var result = Result<object>.MakeSucces(null);

            Assert.AreEqual(result.Success, true);
        }

        [Test]
        public void MakeSuccess_FailFalse()
        {
            var result = Result<object>.MakeSucces(null);

            Assert.AreEqual(result.Fail, false);
        }

        [Test]
        public void MakeSuccess_ErrorsThrow()
        {
            var result = Result<object>.MakeSucces(null);

            Assert.Throws<InvalidOperationException>(() => { var _ = result.Errors; });
        }

        [Test]
        public void MakeFailNull_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => Result<object>.MakeFail(null));
        }

        [Test]
        public void MakeFailEmpty_ErrorsEmpty()
        {
            var result = Result<object>.MakeFail(new string[0]);

            Assert.AreEqual(result.Errors.Count, 0);
        }

        [Test]
        public void MakeFailEmpty_FailTrue()
        {
            var result = Result<object>.MakeFail(new string[0]);

            Assert.AreEqual(result.Fail, true);
        }

        [Test]
        public void MakeFailEmpty_SuccessFalse()
        {
            var result = Result<object>.MakeFail(new string[0]);

            Assert.AreEqual(result.Success, false);
        }

        [Test]
        public void MakeFail_ErrorsFilled()
        {
            var result = Result<object>.MakeFail(new[] { "error" });

            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors[0], "error");
        }

        [Test]
        public void MakeFailErrrorMessage_MessageWrote()
        {
            var result = Result<object>.MakeFailMessage("msg");

            Assert.AreEqual(result.Errors.Count, 1);
            Assert.AreEqual(result.Errors[0], "msg");
        }

        [Test]
        public void MakeFail_FailTrue()
        {
            var result = Result<object>.MakeFail(new[] { "error" });

            Assert.AreEqual(result.Fail, true);
        }

        [Test]
        public void MakeFailErrrorMessage_FailTrue()
        {
            var result = Result<object>.MakeFailMessage("msg");

            Assert.AreEqual(result.Fail, true);
        }

        [Test]
        public void MakeFail_SuccessFalse()
        {
            var result = Result<object>.MakeFail(new[] { "error" });

            Assert.AreEqual(result.Success, false);
        }

        [Test]
        public void MakeFailErrrorMessage_SuccessFalse()
        {
            var result = Result<object>.MakeFailMessage("msg");

            Assert.AreEqual(result.Success, false);
        }

        [Test]
        public void MakeFail_DataThrow()
        {
            var result = Result<object>.MakeFail(new[] { "error" });

            Assert.Throws<InvalidOperationException>(() => { var _ = result.Data; });
        }

        [Test]
        public void MakeFailErrorsEmpty_DataThrow()
        {
            var result = Result<object>.MakeFail(new string[0]);

            Assert.Throws<InvalidOperationException>(() => { var _ = result.Data; });
        }

        [Test]
        public void MakeFailErrrorMessage_DataThrow()
        {
            var result = Result<object>.MakeFailMessage("msg");

            Assert.Throws<InvalidOperationException>(() => { var _ = result.Data; });
        }
    }
}