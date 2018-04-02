// Author(s): Paul Calande
// Interface for an IndexedList.

public interface IIndexedList<T>
{
    int Add(T value);
    void Remove(int key);
    void Set(int key, T value);
}