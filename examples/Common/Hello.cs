using System;

namespace Common
{
    public class Hello : IHello
    {
        public void SayHello(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
