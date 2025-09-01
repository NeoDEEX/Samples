namespace SimpleEncryptDecrypt;

internal class SimplePoco
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }

    public override string ToString()
    {
        return $"[({this.Id}) {this.Name}/{this.Password}]";
    }
}
