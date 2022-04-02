using System.ComponentModel.DataAnnotations;

namespace wms.Dto.Common.Responses;

public class ResponseMetaData
{
    [Required] public string time { get; set; }

    public string? message { get; set; }
}