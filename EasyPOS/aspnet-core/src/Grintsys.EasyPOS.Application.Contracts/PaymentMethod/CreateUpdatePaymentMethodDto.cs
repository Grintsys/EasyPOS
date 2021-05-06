using System;
using System.Collections.Generic;

namespace Grintsys.EasyPOS.PaymentMethod
{
    public class CreateUpdatePaymentMethodDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public CreateUpdateCreditDebitCardDto CreditDebitCard { get; set; }
        public CreateUpdateCashDto Cash { get; set; }
        public CreateUpdateWireTransferDto WireTransfer { get; set; }
        public List<CreateUpdateBankCheckDto> BankChecks { get; set; } = new List<CreateUpdateBankCheckDto>();
    }
}
