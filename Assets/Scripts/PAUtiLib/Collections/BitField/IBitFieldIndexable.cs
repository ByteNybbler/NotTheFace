// Author(s): Paul Calande
// Interface for a bit field.
// TKeyIndex is the type of key used to index the bits within the field.

public interface IBitFieldIndexable<TKeyIndex>
{
    int GetInt();
    void SetIndex(TKeyIndex index);
    void ClearIndex(TKeyIndex index);
    void ToggleIndex(TKeyIndex index);
    bool IsIndexSet(TKeyIndex index);
}