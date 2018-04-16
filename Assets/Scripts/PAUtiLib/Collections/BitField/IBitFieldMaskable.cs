// Author(s): Paul Calande
// Interface for a bit field.
// TKeyMask is the type of the mask used to modify the field.

public interface IBitFieldMaskable<TKeyMask>
{
    int GetInt();
    void SetMask(TKeyMask mask);
    void ClearMask(TKeyMask mask);
    void ToggleMask(TKeyMask mask);
}