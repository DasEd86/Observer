using System;

class ObserveValues : IObserver<int> {
   private IDisposable unsubscriber;
   private string instName;

   public ObserveValues(string name) {
      this.instName = name;
   }

   public string Name
   {  get{ return this.instName; } }

   // Auf übergebenes Observable registrieren.
   // Unsubscriber speichern um Beobachten zu beenden
   public virtual void Subscribe(IObservable<int> provider) {
      if (provider != null) {
         unsubscriber = provider.Subscribe(this);
      }
   }

   // Mitteilung vom Observable, dass keine Werte mehr gesendet werden
   public virtual void OnCompleted() {
      Console.WriteLine("Completed transmitting data to: {0}", this.Name);
      this.Unsubscribe();
   }

   public virtual void OnError(Exception e) { }

   // Beim Observable liegt ein neuer Wert vor
   public virtual void OnNext(int value) {
      Console.WriteLine("Name: {0}. Received: {1}.", this.Name, value);
   }

   // Warten auf neue Werte beenden
   public virtual void Unsubscribe() {
      unsubscriber.Dispose();
   }
}
