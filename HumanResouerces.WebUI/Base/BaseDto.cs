namespace HumanResouerces.WebUI.Base
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }
    }
}
