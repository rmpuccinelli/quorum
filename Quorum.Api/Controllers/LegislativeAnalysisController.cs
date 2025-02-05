namespace Quorum.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Quorum.Application.DTOs;
using Quorum.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]
public class LegislativeAnalysisController : ControllerBase
{
    private readonly IQuorumService _quorumService;

    public LegislativeAnalysisController(IQuorumService quorumService)
    {
        _quorumService = quorumService;
    }
    [HttpGet("legislator/voting-records")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LegislatorAnalysisDto>>> GetLegislatorVotingRecords()
    {
        var records = await _quorumService.GetLegislatorVotingRecordsAsync();
        return Ok(records);
    }

    [HttpGet("bill/support-analysis")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BillAnalysisDto>>> GetBillSupportAnalysis()
    {
        var analysis = await _quorumService.GetBillSupportAnalysisAsync();
        return Ok(analysis);
    }
} 