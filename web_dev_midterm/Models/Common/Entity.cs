namespace web_dev_midterm.Models.Common;

public abstract class Entity
{
    public long Id { get; init; }
    
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
