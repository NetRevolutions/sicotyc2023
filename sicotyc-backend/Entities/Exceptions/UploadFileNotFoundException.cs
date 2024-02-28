namespace Entities.Exceptions
{
    public sealed class UploadFileNotFoundException : Exception
    {
        public UploadFileNotFoundException(string fileType)
            : base($"El tipo de archivo: {fileType} para subir no existe")
        { }
    }
}
