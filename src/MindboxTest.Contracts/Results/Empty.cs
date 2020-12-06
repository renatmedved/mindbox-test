namespace MindboxTest.Contracts.Results
{
    public class Empty
    {
        private static readonly Empty _instance = new Empty();//need only one instance

        private Empty () { }

        public static Empty Instance => _instance;
    }
}
