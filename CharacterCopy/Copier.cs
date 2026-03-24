
namespace CharacaterCopy.Tests;

public class Copier
{
    private ISource _source;
    private IDestination _destination;

    public Copier(ISource source, IDestination destination)
    {
        _source = source;
        _destination = destination;
    }

    public void Copy()
    {
        _destination.WriteChar(_source.ReadChar());
    }
}