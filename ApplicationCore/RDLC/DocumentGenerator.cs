
using ApplicationCore.Entities;
using ApplicationCore.Repository;

using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RDLC
{

    public class DocumentGenerator // : IDocumentGenerator
    {
        ////private readonly IRepository<ApplicationSetting> _applicationSettingService;
        //public DocumentGenerator(IRepository<ApplicationSetting> applicationSettingService)
        //{
        //    _applicationSettingService = applicationSettingService;
        //}

        //public byte[] generateCollateralReleaseLetter(ObligorDto obligor, RDLCParameterModels.CollateralReleaseDocument collateralReleaseDocument, out string filename)
        //{
        //    filename = $"{obligor.FullName}_Release_Document.pdf";


        //    // Create our local report object to build our report on.
        //    var localReport = new LocalReport
        //    {
        //        DisplayName = $"{obligor.FullName} Release Document",
        //        ReportEmbeddedResource = "ApplicationCore.RDLC.collateralReleaseLetter.rdlc",
        //        EnableHyperlinks = true
        //    };

        //    var address = string.Join($",{Environment.NewLine}", collateralReleaseDocument.Address.Split(',').Select(x => x.TrimStart()));

        //    var applicationSetting = _applicationSettingService.Get(x => x.IsActive, false);

        //    var reportParameterCollection = new ReportParameterCollection
        //    {
        //        new ReportParameter("To", collateralReleaseDocument.To),
        //         new ReportParameter("Address", collateralReleaseDocument.Address),
        //         new ReportParameter("Organization", collateralReleaseDocument.Organization),
        //         new ReportParameter("Recipient", collateralReleaseDocument.Recipient),
        //          new ReportParameter("Company_Address", applicationSetting.CompanyAddress),
        //         new ReportParameter("Company_Fax", applicationSetting.CompanyFaxNo),
        //         new ReportParameter("Company_TelNo", applicationSetting.CompanyTelNo),
        //           new ReportParameter("Obligor_Name", obligor.FullName),
        //             new ReportParameter("Obligor_BVN", obligor.BVN != null ? $"BVN No {obligor.BVN}": $"Account No {obligor.AccountNumber}"),
        //               new ReportParameter("Obligor_Branch", obligor.Branch),
        //    };

        //    localReport.SetParameters(reportParameterCollection);


        //    localReport.DataSources.Clear();


        //    Warning[] warnings;
        //    string[] streams;
        //    string mimeType, encoding, fileNameExtension;
        //    var renderedReport = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //    return renderedReport;
        //}

        //public byte[] generateDRAContractExtension(DRAContract contract, List<ObligorDto> obligorAssignments, ApplicationSetting applicationSetting, out string filename)
        //{
        //    filename = $"{contract.DebtRecoveryAgent.DraName}_Contract_Letter.pdf";


        //    // Create our local report object to build our report on.
        //    var localReport = new LocalReport
        //    {
        //        DisplayName = $"{contract.DebtRecoveryAgent.DraName} Contract Letter",
        //        ReportEmbeddedResource = "ApplicationCore.RDLC.engagementLetter.rdlc",
        //        EnableHyperlinks = true
        //    };

        //    var draAddress = string.Join($",{Environment.NewLine}", contract.DebtRecoveryAgent.Address.Split(',').Select(x => x.TrimStart()));

        //    var reportParameterCollection = new ReportParameterCollection
        //    {
        //        new ReportParameter("DRA_Name", contract.DebtRecoveryAgent.DraName),
        //         new ReportParameter("DRA_Address", draAddress),
        //         new ReportParameter("Company_Address", applicationSetting.CompanyAddress),
        //         new ReportParameter("Company_Fax", applicationSetting.CompanyFaxNo),
        //         new ReportParameter("Company_TelNo", applicationSetting.CompanyTelNo),
        //         new ReportParameter("DRA_Commission", contract.DebtRecoveryAgent.Commission.ToString())
        //    };

        //    localReport.SetParameters(reportParameterCollection);


        //    localReport.DataSources.Clear();

        //    var draCollection = new List<DebtRecoveryAgent>() { contract.DebtRecoveryAgent };

        //    var reportDRADataSource = new ReportDataSource("Obligor", obligorAssignments);
        //    localReport.DataSources.Add(reportDRADataSource);

        //    Warning[] warnings;
        //    string[] streams;
        //    string mimeType, encoding, fileNameExtension;
        //    var renderedReport = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //    return renderedReport;
        //}

        //public byte[] generateDRAEngagementLetter(DebtRecoveryAgent dra, List<ObligorDto> obligorAssignments, ApplicationSetting applicationSetting, out string filename)
        //{

        //    filename = $"{dra.DraName}_Engagement_Letter.pdf";


        //    // Create our local report object to build our report on.
        //    var localReport = new LocalReport
        //    {
        //        DisplayName = $"{dra.DraName} Engagement Letter",
        //        ReportEmbeddedResource = "ApplicationCore.RDLC.engagementLetter.rdlc",
        //        EnableHyperlinks = true
        //    };

        //    var draAddress = string.Join($",{Environment.NewLine}", dra.Address.Split(',').Select(x => x.TrimStart()));

        //    var reportParameterCollection = new ReportParameterCollection
        //    {
        //        new ReportParameter("DRA_Name", dra.DraName),
        //         new ReportParameter("DRA_Address", draAddress),
        //         new ReportParameter("Company_Address", applicationSetting.CompanyAddress),
        //         new ReportParameter("Company_Fax", applicationSetting.CompanyFaxNo),
        //         new ReportParameter("Company_TelNo", applicationSetting.CompanyTelNo),
        //         new ReportParameter("DRA_Commission", dra.Commission.ToString())
        //    };

        //    localReport.SetParameters(reportParameterCollection);


        //    localReport.DataSources.Clear();

        //    var draCollection = new List<DebtRecoveryAgent>() { dra };

        //    var reportDRADataSource = new ReportDataSource("Obligor", obligorAssignments);
        //    localReport.DataSources.Add(reportDRADataSource);

        //    Warning[] warnings;
        //    string[] streams;
        //    string mimeType, encoding, fileNameExtension;
        //    var renderedReport = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //    return renderedReport;
        //}

        //public byte[] generateInstructionToBank(ObligorDto obligor, RDLCParameterModels.InstructionToBank instructionToBank, out string filename)
        //{
        //    filename = $"{obligor.FullName}_Release_Document.pdf";
        //    string  RemoveCRMS = " Kindly note also that where the Obligor does not have any security documents in the possession of the Bank relating to the loan facilities granted to them, please ensure that their names are removed from the Credit Risk Management System(“CRMS”) of the Central Bank of Nigeria and that of any other Credit Bureaus.";
        //    string NotifyRemoveCRMS = "We would also appreciate a notification confirming the removal of the name of the obligor from the CRMS.";

        //     // Create our local report object to build our report on.
        //    var localReport = new LocalReport
        //    {
        //        DisplayName = $"{obligor.FullName} Release Document",
        //        ReportEmbeddedResource = "ApplicationCore.RDLC.ObligorInstruction.rdlc",
        //        EnableHyperlinks = true
        //    };

        //    var address = string.Join($",{Environment.NewLine}", instructionToBank.Address.Split(',').Select(x => x.TrimStart()));

        //    var applicationSetting = _applicationSettingService.Get(x => x.IsActive, false);

        //    var reportParameterCollection = new ReportParameterCollection
        //    {
        //        new ReportParameter("To", instructionToBank.To),
        //         new ReportParameter("Address", instructionToBank.Address),
        //         new ReportParameter("Organization", instructionToBank.Organization),
        //         new ReportParameter("Recipient", instructionToBank.Recipient),
        //         new ReportParameter("Company_Address", applicationSetting.CompanyAddress),
        //         new ReportParameter("Company_Fax", applicationSetting.CompanyFaxNo),
        //         new ReportParameter("Company_TelNo", applicationSetting.CompanyTelNo),
        //         new ReportParameter("Obligor_Name", obligor.FullName),
        //         new ReportParameter("Obligor_BVN", obligor.BVN != null ? $"BVN No {obligor.BVN}": $"Account No {obligor.AccountNumber}"),
        //         new ReportParameter("Obligor_Branch", obligor.Branch),
        //          new ReportParameter("DocumentSubject", instructionToBank.DocumentSubject),
        //         new ReportParameter("CollateralDetails", instructionToBank.CollateralDetails),
        //           new ReportParameter("RemoveFromCRMS", instructionToBank.RemoveFromCRMS==true?RemoveCRMS:" " ),

        //        new ReportParameter("CompletionDate", instructionToBank.CompletionDate),
        //         new ReportParameter("Instruction", instructionToBank.Instruction),

        //        new ReportParameter("NotifyRemoveCRMS", instructionToBank.RemoveFromCRMS==true?NotifyRemoveCRMS:" " ),
        //           //Remove from CRMS pending
        //              };
        //    localReport.SetParameters(reportParameterCollection);
        //    localReport.DataSources.Clear();
        //    Warning[] warnings;
        //    string[] streams;
        //    string mimeType, encoding, fileNameExtension;
        //    var renderedReport = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //    return renderedReport;
        //}

        //public byte[] GenerateSettlementAgrrementDocument(ObligorDto obligor, RDLCParameterModels.ResolutionDocument resolutionParams, out string filename)
        //{
        //    filename = $"{obligor.FullName}_Release_Document.pdf";


        //    // Create our local report object to build our report on.
        //    var localReport = new LocalReport
        //    {
        //        DisplayName = $"{obligor.FullName} Resolution Document",
        //        ReportEmbeddedResource = "ApplicationCore.RDLC.settlementAgreement.rdlc",
        //        EnableHyperlinks = true
        //    };

        //    var address = string.Join($",{Environment.NewLine}", obligor.Address.Split(',').Select(x => x.TrimStart()));

        //    var applicationSetting = _applicationSettingService.Get(x => x.IsActive, false);

        //    var reportParameterCollection = new ReportParameterCollection
        //    {
        //        new ReportParameter("ObligorName", obligor.FullName),
        //         new ReportParameter("Address", address),
        //         new ReportParameter("OrganizationName", resolutionParams.Organization),
        //         new ReportParameter("Title", resolutionParams.RecipientTitle),
        //          new ReportParameter("SettlementDate", resolutionParams.SettlementDate),
        //          new ReportParameter("AgreementDueDate", resolutionParams.AgreementDueDate),
        //            new ReportParameter("ResolutionTerms", resolutionParams.ResolutionTerms),
        //          new ReportParameter("Company_Address", applicationSetting.CompanyAddress),
        //         new ReportParameter("Company_Fax", applicationSetting.CompanyFaxNo),
        //         new ReportParameter("Company_TelNo", applicationSetting.CompanyTelNo)
        //    };

        //    localReport.SetParameters(reportParameterCollection);


        //    localReport.DataSources.Clear();


        //    Warning[] warnings;
        //    string[] streams;
        //    string mimeType, encoding, fileNameExtension;
        //    var renderedReport = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

        //    return renderedReport;
        

    }
}
