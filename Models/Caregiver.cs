public class Caregiver
{
    public int Id { get; set; }
    public required string Name { get; set; } // Using 'required' modifier
    public int AssignedPatientId { get; set; }
}
