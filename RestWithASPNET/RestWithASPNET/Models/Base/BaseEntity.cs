using System.Runtime.Serialization;

namespace RestWithASPNET.Models.Base
{
    //Contrato entre atributos
    //[DataContract]
    public class BaseEntity
    {
        public long Id { get; set; }
    }
}
