
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
        var nextChar = _source.ReadChar();
        while(nextChar != '\n')
        {
            _destination.WriteChar(nextChar);
            nextChar = _source.ReadChar();
        }
    }
}