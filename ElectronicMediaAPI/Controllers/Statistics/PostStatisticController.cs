/*********************************************************************
 * 
 * PROPRIETARY and CONFIDENTIAL
 * 
 * This is licensed from, and is trade secret of:
 * 
 *          Group 10 - PRN231 - SU23
 *          FPT University, Education and Training zone
 *          Hoa Lac Hi-tech Park, Km29, Thang Long Highway
 *          Ha Noi, Viet Nam
 *          
 * Refer to your License Agreement for restrictions on use,
 * duplication, or disclosure
 * 
 * RESTRICTED RIGHTS LEGEND
 * 
 * Use, duplication or disclosure is the
 * subject to restriction in Articles 736 and 738 of the 
 * 2005 Civil Code, the Intellectual Property Law and Decree 
 * No. 85/2011/ND-CP amending and supplementing a number of 
 * articles of Decree 100/ND-CP/2006 of the Government of Viet Nam
 * 
 * 
 * Copy right 2023 - PRN231 - SU23 - Group 10. All Rights Reserved
 * 
 * Unpublished - All rights reserved under the copyright laws 
 * of the Government of Viet Nam
*********************************************************************/

using ElectronicMedia.Core.Repository.Models;
using ElectronicMedia.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMediaAPI.Controllers.Analists
{
    [Route("api/statistic/post")]
    [ApiController]
    public class PostStatisticController : ControllerBase
    {
        private readonly IPostStatisticService _postStatisticService;
        private readonly ILogger<PostStatisticController> _logger;
        public PostStatisticController(IPostStatisticService postStatisticService,
            ILogger<PostStatisticController> logger)
        {
            _postStatisticService = postStatisticService;
            _logger = logger;
        }

        [HttpGet("{writerId}")]
        public async Task<PostStatisticModel> GetToltalPostOfWriterInMonth([FromRoute] Guid writerId)
        {
            try
            {
                var result = await _postStatisticService.GetTotalPostForEachWriter(writerId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when get total post of writer in month with userId {writerId}", ex);
                return new PostStatisticModel()
                {
                    Month = 0,
                    NumberPost = 0,
                };
            }
        }
    }
}
