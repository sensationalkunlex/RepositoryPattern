
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RDLC
{
    public class RDLCParameterModels
    {
        public class CollateralReleaseDocument
        {
            public int obligorId { get; set; }

            public string ObligorName { get; set; }

            public string To { get; set; }

            public string Address { get; set; }

            public string Organization { get; set; }

            public string Recipient { get; set; }
        }

        public class ResolutionDocument
        {
            public int ObligorId { get; set; }

            public string ObligorName { get; set; }

            public string RecipientEmail { get; set; }
            public string Recipient { get; set; }

            public string RecipientTitle { get; set; }

            public string Organization { get; set; }

            public DateTime DateSettled { get; set; }

            public string SettlementDate { get; set; } 

            public string AgreementDueDate { get; set; }

            public string ResolutionTerms { get; set; }

            public string TotalProposedAmount { get; set; }

            public string ProposalComment { get; set; }

            //public List<PaymentDto> Payments{ get; set; }
        }

        public class InstructionToBank
        {
            public int ObligorId { get; set; }

            public string ObligorName { get; set; }

            public string To { get; set; }

            public string Address { get; set; }

            public string Organization { get; set; }
            public string Recipient { get; set; }
            public string DocumentSubject { get;  set; }
            public string CollateralDetails { get;  set; }
            public string CompletionDate { get; set; }
            public string Instruction { get; set; }
            public bool RemoveFromCRMS { get;  set; }
        }
    }
}
