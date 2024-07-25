using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commons;

public class EmailTemplate
{
    public const string NEW_PASSWORD_SUBJECT= "New Password";
    public const string NEW_PASSWORD_BODY= "Hi,<br/><br/>Your new password is {0}";
}
