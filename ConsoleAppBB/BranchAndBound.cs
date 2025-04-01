namespace ConsoleAppBB;

public interface ISolution
{
    float bound();
    bool isComplete();
    List<ISolution> sons();
}

public static class BranchAndBound
{
    public static ISolution? Run(ISolution initial)
    {
        float best_cost = float.PositiveInfinity;
        ISolution? best_solution = null;
        PriorityQueue<ISolution, float> Z = new(Comparer<float>.Create((x, y) => x < y ? -1 : 1));
        Z.Enqueue(initial, initial.bound());
        while (Z.Count > 0)
        {
            ISolution w;
            float w_bound;
            Z.TryDequeue(out w, out w_bound);
            if (w_bound < best_cost)
            {
                List<ISolution> candidates = w.sons();
                foreach (ISolution c in candidates)
                {
                    float c_bound = c.bound();
                    if (c_bound < best_cost)
                    {
                        if (c.isComplete())
                        {
                            best_cost = c_bound;
                            best_solution = c;
                        }
                        else
                        {
                            Z.Enqueue(c, c_bound);
                        }
                    }
                }
            }
        }
        return best_solution;
    }
}                            