// Author(s): Paul Calande
// A collection of bits stored as an integer.

public class BitField : IBitFieldMaskable<int>, IBitFieldIndexable<int>
{
    // The field, as an integer.
    int field = 0;

    // Constructor, which sets the initial indices based on the values.
    public BitField(params int[] indices)
    {
        foreach (int index in indices)
        {
            SetIndex(index);
        }
    }

    // Gets the integer value of a field with only the given bit index set.
    private int ToPowerOf2(int index)
    {
        return 1 << index;
    }

    // Returns the current integer value of the bit field.
    public int GetInt()
    {
        return field;
    }

    // Forcefully sets the integer value of the bit field.
    public void SetInt(int field)
    {
        this.field = field;
    }

    public void SetMask(int mask)
    {
        field |= mask;
    }

    public void ClearMask(int mask)
    {
        field &= ~mask;
    }

    public void ToggleMask(int mask)
    {
        field ^= mask;
    }

    // Sets the bit with the given index.
    public void SetIndex(int index)
    {
        SetMask(ToPowerOf2(index));
    }

    // Clears the bit with the given index.
    public void ClearIndex(int index)
    {
        ClearMask(ToPowerOf2(index));
    }

    // Toggles the bit with the given index.
    public void ToggleIndex(int index)
    {
        ToggleMask(ToPowerOf2(index));
    }

    // Returns true if the given index is set.
    public bool IsIndexSet(int index)
    {
        return (field & ToPowerOf2(index)) != 0;
    }
}