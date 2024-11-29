public class Patient
{
    public int Id { get; set; }
    public required string Name { get; set; } // Using 'required' modifier
    public required string Address { get; set; } // Using 'required' modifier
    public required string MedicalRecords { get; set; } // Using 'required' modifier
}
