using System;
using System.Collections.Generic;

// Observer interface
public interface IObserver
{
    void Update(string message);
}

// Subject class
public class Subject
{
    private List<IObserver> observers = new List<IObserver>();

    // Attach an observer
    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    // Detach an observer
    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    // Notify all observers
    public void Notify(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }
}

// Concrete implementation of technician
public class Technician : IObserver
{
    private string name;

    public Technician(string name)
    {
        this.name = name;
    }

    // Update method to receive notifications
    public void Update(string message)
    {
        Console.WriteLine($"Technician {name} received message: {message}");
    }
}

// Abstract class for carrier capability
public abstract class Carrier
{
    public abstract string Carry();
}

// Abstract class for engine size
public abstract class Engine
{
    public abstract string Power();
}

// Abstract class for towing capability
public abstract class Towing
{
    public abstract string Tow();
}

// Abstract class for additional features
public abstract class Feature
{
    public abstract int Cost();
    public abstract string Description();
}

// Concrete implementation of a vehicle
public class Vehicle
{
    protected Carrier carrierCapability;
    protected Engine engine;
    protected Towing towingCapability;
    protected List<Feature> additionalFeatures = new List<Feature>();
    //protected Subject subject = new Subject();
    public Subject subject = new Subject();

    // Constructor to initialize vehicle capabilities
    public Vehicle(Carrier carrier, Engine engine, Towing towing)
    {
        carrierCapability = carrier;
        this.engine = engine;
        towingCapability = towing;
    }

    // Assemble the vehicle
    public void Assemble()
    {
        // Assemble the vehicle
        Console.WriteLine("Assembling vehicle...");
    }

    // Add a feature to the vehicle
    public void AddFeature(Feature feature)
    {
        additionalFeatures.Add(feature);
        NotifyTechnicians($"Feature '{feature.Description()}' added to the vehicle.");
    }

    // Get the description of the vehicle
    public string GetDescription()
    {
        // Generate description including all features
        string description = $"Vehicle with {carrierCapability.Carry()} capability, {engine.Power()} engine, {towingCapability.Tow()} towing, and additional features:\n";
        foreach (var feature in additionalFeatures)
        {
            description += $"- {feature.Description()} (Cost: {feature.Cost()}).\n";
        }
        return description;
    }

    // Notify technicians about changes
    private void NotifyTechnicians(string message)
    {
        subject.Notify(message);
    }
}

// Concrete implementation of a sound system feature
// Concrete implementation of a sound system feature
public class SoundSystem : Feature
{
    protected Feature baseFeature;

    public SoundSystem(Feature baseFeature)
    {
        this.baseFeature = baseFeature;
    }

    // Calculate cost including the base feature
    public override int Cost()
    {
        return (baseFeature != null ? baseFeature.Cost() : 0) + 1000; // Example cost for adding a sound system
    }

    // Generate description including the base feature
    public override string Description()
    {
        return (baseFeature != null ? baseFeature.Description() : "") + ", Sound System";
    }
}


// Concrete implementation of a Wi-Fi feature
public class WiFi : Feature
{
    protected Feature baseFeature;

    public WiFi(Feature baseFeature)
    {
        this.baseFeature = baseFeature;
    }

    // Calculate cost including the base feature
    public override int Cost()
    {
        return baseFeature.Cost() + 750; // Example cost for adding Wi-Fi
    }

    // Generate description including the base feature
    public override string Description()
    {
        return baseFeature.Description() + ", Wi-Fi";
    }
}

// Concrete implementation of an assist camera feature
public class AssistCamera : Feature
{
    protected Feature baseFeature;

    public AssistCamera(Feature baseFeature)
    {
        this.baseFeature = baseFeature;
    }

    // Calculate cost including the base feature
    public override int Cost()
    {
        return baseFeature.Cost() + 200; // Example cost for adding an assist camera
    }

    // Generate description including the base feature
    public override string Description()
    {
        return baseFeature.Description() + ", Assist Camera";
    }
}

// Concrete implementation of carrier capabilities
public class GoodAndDriver : Carrier
{
    public override string Carry()
    {
        return "Good and Driver";
    }
}

public class TwoPeopleMaxAndBag : Carrier
{
    public override string Carry()
    {
        return "2 people max, and bag";
    }
}

public class FivePeopleMaxAndFewLuggage : Carrier
{
    public override string Carry()
    {
        return "5 people max and few luggage";
    }
}

public class TwentyPeopleMax : Carrier
{
    public override string Carry()
    {
        return "20 people max";
    }
}

public class SixtyFivePeopleMax : Carrier
{
    public override string Carry()
    {
        return "65 people max";
    }
}

// Concrete implementation of engine sizes
public class SmallEngine : Engine
{
    public override string Power()
    {
        return "Small";
    }
}

public class MediumEngine : Engine
{
    public override string Power()
    {
        return "Medium";
    }
}

public class LargeEngine : Engine
{
    public override string Power()
    {
        return "Large";
    }
}

public class ExtraLargeEngine : Engine
{
    public override string Power()
    {
        return "Extra Large";
    }
}

// Concrete implementation of towing capabilities
public class CanTow : Towing
{
    public override string Tow()
    {
        return "Can Tow";
    }
}

public class CannotTow : Towing
{
    public override string Tow()
    {
        return "Cannot Tow";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create technicians
        var bikeTechnician = new Technician("Bike Tech");
        var lightVehicleTechnician = new Technician("Light Vehicle Tech");
        var heavyVehicleTechnician = new Technician("Heavy Vehicle Tech");

        // Create a vehicle with specific capabilities
        Vehicle vehicle = new Vehicle(new GoodAndDriver(), new MediumEngine(), new CanTow());

        // Attach technicians to the subject
        vehicle.subject.Attach(bikeTechnician);
        vehicle.subject.Attach(lightVehicleTechnician);
        vehicle.subject.Attach(heavyVehicleTechnician);

        // Assemble the vehicle
        vehicle.Assemble();

        // Add features dynamically
        Feature baseFeature = new WiFi(new AssistCamera(new SoundSystem(null)));
        vehicle.AddFeature(baseFeature);

        // Get the description of the vehicle
        string description = vehicle.GetDescription();
        Console.WriteLine(description);
    }
}
