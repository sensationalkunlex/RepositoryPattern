
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApplicationCore.RDLC.RDLCParameterModels;

namespace ApplicationCore.RDLC
{
    public interface IDocumentGenerator
    {
        //byte[] generateDRAEngagementLetter(DebtRecoveryAgent dra, List<ObligorDto> obligorAssignments, ApplicationSetting applicationSetting, out string filename);

        //byte[] generateDRAContractExtension(DRAContract contract, List<ObligorDto> obligorAssignments, ApplicationSetting applicationSetting, out string filename);

        //byte[] generateCollateralReleaseLetter(ObligorDto obligor, CollateralReleaseDocument collateralReleaseDocument, out string filename);

        //byte[] GenerateSettlementAgrrementDocument(ObligorDto obligor, RDLCParameterModels.ResolutionDocument resolutionParams, out string filename);

        //byte[] generateInstructionToBank(ObligorDto obligor, InstructionToBank instructionToBank, out string filename);

    }
}
