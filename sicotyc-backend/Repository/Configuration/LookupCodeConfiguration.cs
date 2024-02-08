using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class LookupCodeConfiguration : IEntityTypeConfiguration<LookupCode>
    {
        public void Configure(EntityTypeBuilder<LookupCode> builder)
        {
            builder.HasData
            (
                // TIPO DE PAGO PEAJE
                new LookupCode
                {
                    Id = new Guid("867C1549-7132-4E8E-174A-08DA70AE983A"),
                    LookupCodeValue = "ByAxis",
                    LookupCodeName = "Por Eje",
                    LookupCodeOrder = 1,
                    LookupCodeGroupId = new Guid("71B0316A-9831-499A-B9BB-08DA70AE70ED"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                { 
                    Id = new Guid("7E603067-A1ED-4B52-174B-08DA70AE983A"),
                    LookupCodeValue = "ByAxis2",
                    LookupCodeName = "Por Eje2",
                    LookupCodeOrder = 2,
                    LookupCodeGroupId = new Guid("71B0316A-9831-499A-B9BB-08DA70AE70ED"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                { 
                    Id = new Guid("1A011E51-2471-4CCD-174C-08DA70AE983A"),
                    LookupCodeValue = "ByAxis3",
                    LookupCodeName = "Por Eje3",
                    LookupCodeOrder = 3,
                    LookupCodeGroupId = new Guid("71B0316A-9831-499A-B9BB-08DA70AE70ED"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                { 
                    Id = new Guid("23078793-CD0A-4718-2AA4-08DA71DA4714"),
                    LookupCodeValue = "ByAxis4",
                    LookupCodeName = "Por Eje4",
                    LookupCodeOrder = 4,
                    LookupCodeGroupId = new Guid("71B0316A-9831-499A-B9BB-08DA70AE70ED"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("47B84A27-C75A-44D3-174D-08DA70AE983A"),
                    LookupCodeValue = "ByAxis5",
                    LookupCodeName = "Por Eje5",
                    LookupCodeOrder = 5,
                    LookupCodeGroupId = new Guid("71B0316A-9831-499A-B9BB-08DA70AE70ED"),
                    CreatedBy = "SYSTEM"
                },
                // TIPO DE DOC. IDENTIDAD
                new LookupCode
                { 
                    Id = new Guid("2D253E01-AFA1-4A59-BC6A-26526F0D8498"),
                    LookupCodeValue = "DNI",
                    LookupCodeName = "Documento Nacional de Identidad",
                    LookupCodeOrder = 1,
                    LookupCodeGroupId = new Guid("86D227DC-E0CA-4A78-85F4-83A6EB30CBC7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                { 
                    Id = new Guid("8DC0180A-2FFC-4807-803A-37AAB6ECAAB2"),
                    LookupCodeValue = "CEX",
                    LookupCodeName = "Carnet de Extranjería",
                    LookupCodeOrder = 2,
                    LookupCodeGroupId = new Guid("86D227DC-E0CA-4A78-85F4-83A6EB30CBC7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                { 
                    Id = new Guid("DE0CC597-AD66-4497-ACAB-33617EB077BD"), 
                    LookupCodeValue = "PASS",
                    LookupCodeName = "Pasaporte",
                    LookupCodeOrder = 3,
                    LookupCodeGroupId = new Guid("86D227DC-E0CA-4A78-85F4-83A6EB30CBC7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                { 
                    Id = new Guid("792F255C-2B8B-42E6-9968-2855373E5C86"),
                    LookupCodeValue = "PNAC",
                    LookupCodeName = "Partida de Nacimiento",
                    LookupCodeOrder = 4,
                    LookupCodeGroupId = new Guid("86D227DC-E0CA-4A78-85F4-83A6EB30CBC7"),
                    CreatedBy = "SYSTEM"                    
                },
                new LookupCode
                {
                    Id = new Guid("B2A7D680-B5DC-41D1-9792-695602FC2954"),
                    LookupCodeValue = "CFFAA",
                    LookupCodeName = "Carnet de FFAA",
                    LookupCodeOrder = 5,
                    LookupCodeGroupId = new Guid("86D227DC-E0CA-4A78-85F4-83A6EB30CBC7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("FE8B2536-5A20-4680-8DFE-526000DF87E1"),
                    LookupCodeValue = "PASSD",
                    LookupCodeName = "Pasaporte Diplomatico",
                    LookupCodeOrder = 6,
                    LookupCodeGroupId = new Guid("86D227DC-E0CA-4A78-85F4-83A6EB30CBC7"),
                    CreatedBy = "SYSTEM"
                },
                // TIPO DE EMPRESA
                new LookupCode
                {
                    Id = new Guid("EAF628EE-9413-472E-A5B7-3C9D45F10CF0"),
                    LookupCodeValue = "ET",
                    LookupCodeName = "Empresa de Transporte",
                    LookupCodeOrder = 1,
                    LookupCodeGroupId = new Guid("E4D10BC8-A160-4A9D-BC87-C94CF849E14C"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("58250D62-975A-4883-81F7-946C91CF2DEC"),
                    LookupCodeValue = "OT",
                    LookupCodeName = "Otros",
                    LookupCodeOrder = 2,
                    LookupCodeGroupId = new Guid("E4D10BC8-A160-4A9D-BC87-C94CF849E14C"),
                    CreatedBy = "SYSTEM"
                },
                // TIPO DE SERVICIO
                new LookupCode
                {
                    Id = new Guid("6963984F-C5E0-4ED9-9647-46AC7054E344"),
                    LookupCodeValue = "IMPO",
                    LookupCodeName = "IMPORTACION",
                    LookupCodeOrder = 1,
                    LookupCodeGroupId = new Guid("C6ED82D5-4A24-464B-BEBD-F33C0B7F7D80"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("E83581FC-E05C-4C80-B5C2-E381FD7765D7"),
                    LookupCodeValue = "EXPO",
                    LookupCodeName = "EXPORTACION",
                    LookupCodeOrder = 2,
                    LookupCodeGroupId = new Guid("C6ED82D5-4A24-464B-BEBD-F33C0B7F7D80"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("5F38D3FD-F34E-45EB-AEBF-512F5EBD94EE"),
                    LookupCodeValue = "CS",
                    LookupCodeName = "CARGA SUELTA",
                    LookupCodeOrder = 3,
                    LookupCodeGroupId = new Guid("C6ED82D5-4A24-464B-BEBD-F33C0B7F7D80"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("FDC11A23-1DC7-4160-BB9D-019579C56E46"),
                    LookupCodeValue = "DV",
                    LookupCodeName = "DEVOLUCIÓN DE VACÍO",
                    LookupCodeOrder = 4,
                    LookupCodeGroupId = new Guid("C6ED82D5-4A24-464B-BEBD-F33C0B7F7D80"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("E5C70DF3-CF54-477F-881D-7D142F0B51AA"),
                    LookupCodeValue = "TX",
                    LookupCodeName = "TRACCIÓN",
                    LookupCodeOrder = 5,
                    LookupCodeGroupId = new Guid("C6ED82D5-4A24-464B-BEBD-F33C0B7F7D80"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("8BD83659-B611-488D-AAAC-E5D418BAC06C"),
                    LookupCodeValue = "CB",
                    LookupCodeName = "CAMA BAJA",
                    LookupCodeOrder = 6,
                    LookupCodeGroupId = new Guid("C6ED82D5-4A24-464B-BEBD-F33C0B7F7D80"),
                    CreatedBy = "SYSTEM"
                },
                // CLAIMS PERMITIDOS
                new LookupCode
                { 
                    Id = new Guid("752CE625-DA67-4842-B19D-18C5572DBBCE"),
                    LookupCodeValue = "UserName",
                    LookupCodeName = "USERNAME",
                    LookupCodeOrder = 1,
                    LookupCodeGroupId = new Guid("CDA56E87-1B44-4625-9F19-AC7EB282A9B7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("37A294BB-D8E2-4655-80A8-A2FE719766D4"),
                    LookupCodeValue = "FirstName",
                    LookupCodeName = "FIRSTNAME",
                    LookupCodeOrder = 2,
                    LookupCodeGroupId = new Guid("CDA56E87-1B44-4625-9F19-AC7EB282A9B7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("E129C250-DE59-45D3-8794-58E073FF8064"),
                    LookupCodeValue = "LastName",
                    LookupCodeName = "LASTNAME",
                    LookupCodeOrder = 3,
                    LookupCodeGroupId = new Guid("CDA56E87-1B44-4625-9F19-AC7EB282A9B7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("6B1B516F-9073-4657-8A4C-9CB7EBE8EA25"),
                    LookupCodeValue = "Email",
                    LookupCodeName = "EMAIL",
                    LookupCodeOrder = 4,
                    LookupCodeGroupId = new Guid("CDA56E87-1B44-4625-9F19-AC7EB282A9B7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("1AEC098A-859A-4586-80B6-B6F4BEB848FB"),
                    LookupCodeValue = "Id",
                    LookupCodeName = "ID",
                    LookupCodeOrder = 5,
                    LookupCodeGroupId = new Guid("CDA56E87-1B44-4625-9F19-AC7EB282A9B7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("50BD3490-2377-4945-9229-F018F6B07BF8"),
                    LookupCodeValue = "PhoneNumber",
                    LookupCodeName = "PHONENUMBER",
                    LookupCodeOrder = 6,
                    LookupCodeGroupId = new Guid("CDA56E87-1B44-4625-9F19-AC7EB282A9B7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("F7AB3CF1-AFE9-4B2B-977F-953D9F3B9275"),
                    LookupCodeValue = "Role",
                    LookupCodeName = "ROLE",
                    LookupCodeOrder = 7,
                    LookupCodeGroupId = new Guid("CDA56E87-1B44-4625-9F19-AC7EB282A9B7"),
                    CreatedBy = "SYSTEM"
                },
                new LookupCode
                {
                    Id = new Guid("8E009C6D-5920-4135-8A26-49EC04C6E7D5"),
                    LookupCodeValue = "Img",
                    LookupCodeName = "IMG",
                    LookupCodeOrder = 8,
                    LookupCodeGroupId = new Guid("CDA56E87-1B44-4625-9F19-AC7EB282A9B7"),
                    CreatedBy = "SYSTEM"
                }
            );
        }
    }
}
