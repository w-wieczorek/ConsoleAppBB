using ConsoleAppBB;

var input = Console.ReadLine();
if (input is not null)
{
    int n = int.Parse(input);
    Seq? solution = BranchAndBound.Run(new Seq(n, null, 1)) as Seq;
    if (solution is not null)
    {
        Console.WriteLine(solution);
    }
}

record LinkListNode(int Number, LinkListNode? Parent);

class Seq : ISolution
{
    private LinkListNode _last;
    private int _goal;

    private int ListLength()
    {
        int counter = 0;
        LinkListNode? current = _last;
        while (current is not null)
        {
            ++counter;
            current = current.Parent;
        }
        return counter;
    }
    
    public Seq(int goal, LinkListNode? previous, int nextNumber)
    {
        _goal = goal;
        _last = new LinkListNode(nextNumber, previous);
    }
        
    public float bound()
    {
        int size = ListLength();
        int counter = 0;
        int x = _last.Number;
        while (x < _goal)
        {
            counter++;
            x *= 2;
        }
        return size + counter;
    }

    public bool isComplete()
    {
        return _goal == _last.Number;
    }

    public List<ISolution> sons()
    {
        List<ISolution> result = new();
        LinkListNode? current = _last;
        while (current is not null)
        {
            result.Add(new Seq(_goal, _last, current.Number + _last.Number));
            current = current.Parent;
        }
        return result;
    }

    public override string ToString()
    {
        string s = "";
        LinkListNode? current = _last;
        while (current is not null)
        {
            s += $"{current.Number} ";
            current = current.Parent;
        }
        return s;
    }
}