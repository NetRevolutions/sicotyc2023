namespace Entities.Enum
{
    public enum CompanyStateEnum
    {
        [StringValue("Activo")]
        ACTIVO,
        [StringValue("Obligado a llevar contabilidad")]
        OBLIGADO_LLEVAR_CONTABILIDAD,
        [StringValue("Baja Temporal")]
        BAJA_TEMPORAL,
        [StringValue("Baja Definitiva")]
        BAJA_DEFINITIVA,
        [StringValue("Omiso")]
        OMISO,
        [StringValue("Obligado a emitir comprobantes electronicos")]
        OBLIGADO_EMITIR_COMPROBANTE_ELECTRONICO
    }
}
