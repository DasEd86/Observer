using System;

class Program
{
    static void Main(string[] args)
    {
      ValueGenerator provider = new ValueGenerator();

      ObserveValues observer1 = new ObserveValues("Observer 1");
      observer1.Subscribe(provider);

      ObserveValues observer2 = new ObserveValues("Observer 2");
      observer2.Subscribe(provider);

      provider.doSomething();
      provider.doSomething();
      provider.doSomething();
      provider.stop();
    }
}
