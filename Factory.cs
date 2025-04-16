namespace Projet;


public abstract class InterventionFactory : Intervention
{
    public abstract IIntervention CreerIntervention();
}

public class MaintenanceFactory : InterventionFactory
{
    public override IIntervention CreerIntervention() => new MaintenanceIntervention();
}

public class UrgenceFactory : InterventionFactory
{
    public override IIntervention CreerIntervention() => new UrgenceIntervention();
}

    abstract class Creator
    {
        public abstract IProduct FactoryMethod();

        public string SomeOperation()
        {
            // Call the factory method to create a Product object.
            var product = FactoryMethod();
            // Now, use the product.
            var result = "Creator: The same creator's code has just worked with "
                + product.Operation();

            return result;
        }
    }

    class CorrectifCreator : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new InterventionCorrectif();
        }
    }

    class PreventifCreator : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new InterventionPreventif();
        }
    }

    public interface IProduct
    {
        string Operation();
    }

    class InterventionCorrectif : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct1}";
        }
    }

    class InterventionPreventif : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct2}";
        }
    }

    class Client
    {
        public void Main()
        {
            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new CorrectifCreator());

            Console.WriteLine("");

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new PreventifCreator());
        }

        public void ClientCode(Creator creator)
        {
            Console.WriteLine("Client: I'm not aware of the creator's class," +
                "but it still works.\n" + creator.SomeOperation());
        }
    }
