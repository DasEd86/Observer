using System;

class ObserveValues : IObserver<int> {
   private IDisposable unsubscriber;
   private string instName;

   public ObserveValues(string name) {
      this.instName = name;
   }

   public string Name
   {  get{ return this.instName; } }

   public virtual void Subscribe(IObservable<int> provider) {
      if (provider != null) {
         unsubscriber = provider.Subscribe(this);
      }
   }

   public virtual void OnCompleted() {
      Console.WriteLine("Completed transmitting data to: {0}", this.Name);
      this.Unsubscribe();
   }

   public virtual void OnError(Exception e) { }

   public virtual void OnNext(int value) {
      Console.WriteLine("Name: {0}. Received: {1}.", this.Name, value);
   }

   public virtual void Unsubscribe() {
      unsubscriber.Dispose();
   }
}
