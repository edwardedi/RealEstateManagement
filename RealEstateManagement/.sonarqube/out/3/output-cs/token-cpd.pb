œ
KC:\Users\cezar\desktop\RealEstateManagement\Identity\DependencyInjection.cs
	namespace 	
Identity
 
{ 
public 

static 
class 
DependencyInjection +
{ 
public 
static 
IServiceCollection (
AddIdentity) 4
(4 5
this5 9
IServiceCollection: L
servicesM U
,U V
IConfigurationW e
configurationf s
)s t
{ 	
services 
. 
AddDbContext %
<% & 
ApplicationDbContext& :
>: ;
(; <
options< C
=>D F
options 
. 
	UseNpgsql !
(! "
configuration !
.! "
GetConnectionString" 5
(5 6
$str6 I
)I J
,J K
b 
=> 
b 
. 
MigrationsAssembly -
(- .
typeof. 4
(4 5 
ApplicationDbContext5 I
)I J
.J K
AssemblyK S
.S T
FullNameT \
)\ ]
)] ^
)^ _
;_ `
var 
key 
= 
Encoding 
. 
ASCII $
.$ %
GetBytes% -
(- .
$str. U
)U V
;V W
services 
. 
AddAuthentication &
(& '
options' .
=>/ 1
{ 
options 
. %
DefaultAuthenticateScheme 1
=2 3
JwtBearerDefaults4 E
.E F 
AuthenticationSchemeF Z
;Z [
options 
. "
DefaultChallengeScheme .
=/ 0
JwtBearerDefaults1 B
.B C 
AuthenticationSchemeC W
;W X
} 
) 
. 
AddJwtBearer 
( 
options !
=>" $
{ 
options 
. %
TokenValidationParameters 1
=2 3
new4 7%
TokenValidationParameters8 Q
{ 
ValidateIssuer   "
=  # $
false  % *
,  * +
ValidateAudience!! $
=!!% &
false!!' ,
,!!, -
ValidateLifetime"" $
=""% &
true""' +
,""+ ,$
ValidateIssuerSigningKey## ,
=##- .
true##/ 3
,##3 4
IssuerSigningKey$$ $
=$$% &
new$$' * 
SymmetricSecurityKey$$+ ?
($$? @
key$$@ C
)$$C D
}%% 
;%% 
}&& 
)&& 
;&& 
services(( 
.(( 
	AddScoped(( 
<(( 
IUserAuthRepository(( 2
,((2 3
UserAuthRepository((4 F
>((F G
(((G H
)((H I
;((I J
return** 
services** 
;** 
}++ 	
},, 
}-- Ä4
WC:\Users\cezar\desktop\RealEstateManagement\Identity\Repositories\UserAuthRepository.cs
	namespace 	
Identity
 
. 
Repositories 
{ 
public 

class 
UserAuthRepository #
:$ %
IUserAuthRepository& 9
{ 
private 
readonly  
ApplicationDbContext -
context. 5
;5 6
private 
readonly 
IConfiguration '
configuration( 5
;5 6
public 
UserAuthRepository !
(! " 
ApplicationDbContext" 6
context7 >
,> ?
IConfiguration@ N
configurationO \
)\ ]
{ 	
this 
. 
context 
= 
context "
;" #
this 
. 
configuration 
=  
configuration! .
;. /
} 	
public 
async 
Task 
< 
Result  
<  !
string! '
>' (
>( )
Login* /
(/ 0
User0 4
user5 9
)9 :
{ 	
var 
existingUser 
= 
await $
context% ,
., -
Users- 2
.2 3 
SingleOrDefaultAsync3 G
(G H
uH I
=>J L
uM N
.N O
EmailO T
==U W
userX \
.\ ]
Email] b
)b c
;c d
if 
( 
existingUser 
== 
null  $
||% '
!( )
BCrypt) /
./ 0
Net0 3
.3 4
BCrypt4 :
.: ;
Verify; A
(A B
userB F
.F G
PasswordHashG S
,S T
existingUserU a
.a b
PasswordHashb n
)n o
)o p
{ 
return 
Result 
< 
string $
>$ %
.% &
Failure& -
(- .
$str. I
)I J
;J K
} 
var!! 
tokenHandler!! 
=!! 
new!! "#
JwtSecurityTokenHandler!!# :
(!!: ;
)!!; <
;!!< =
var"" 
key"" 
="" 
Encoding"" 
."" 
ASCII"" $
.""$ %
GetBytes""% -
(""- .
configuration"". ;
[""; <
$str""< E
]""E F
!""F G
)""G H
;""H I
var## 
tokenDescriptor## 
=##  !
new##" %#
SecurityTokenDescriptor##& =
{$$ 
Subject%% 
=%% 
new%% 
ClaimsIdentity%% ,
(%%, -
new%%- 0
[%%0 1
]%%1 2
{&& 
new'' 
Claim'' 
('' 

ClaimTypes'' (
.''( )
NameIdentifier'') 7
,''7 8
existingUser''9 E
.''E F
UserId''F L
.''L M
ToString''M U
(''U V
)''V W
)''W X
,''X Y
new(( 
Claim(( 
((( 
$str(( $
,(($ %
existingUser((& 2
.((2 3
Name((3 7
??((8 :
string((; A
.((A B
Empty((B G
)((G H
,((H I
new)) 
Claim)) 
()) 

ClaimTypes)) (
.))( )
Email))) .
,)). /
existingUser))0 <
.))< =
Email))= B
)))B C
,))C D
new** 
Claim** 
(** 
$str** ,
,**, -
existingUser**. :
.**: ;
PhoneNumber**; F
??**G I
string**J P
.**P Q
Empty**Q V
)**V W
}++ 
)++ 
,++ 
Expires,, 
=,, 
DateTime,, "
.,," #
UtcNow,,# )
.,,) *
AddDays,,* 1
(,,1 2
$num,,2 3
),,3 4
,,,4 5
SigningCredentials-- "
=--# $
new--% (
SigningCredentials--) ;
(--; <
new--< ? 
SymmetricSecurityKey--@ T
(--T U
key--U X
)--X Y
,--Y Z
SecurityAlgorithms--[ m
.--m n 
HmacSha256Signature	--n 
)
-- ‚
}.. 
;.. 
var00 
token00 
=00 
tokenHandler00 $
.00$ %
CreateToken00% 0
(000 1
tokenDescriptor001 @
)00@ A
;00A B
return11 
Result11 
<11 
string11  
>11  !
.11! "
Success11" )
(11) *
tokenHandler11* 6
.116 7

WriteToken117 A
(11A B
token11B G
)11G H
)11H I
;11I J
}22 	
public44 
async44 
Task44 
<44 
Result44  
<44  !
Guid44! %
>44% &
>44& '
Register44( 0
(440 1
User441 5
user446 :
,44: ;
CancellationToken44< M
cancellationToken44N _
)44_ `
{55 	
var66 
existingUser66 
=66 
await66 $
context66% ,
.66, -
Users66- 2
.662 3 
SingleOrDefaultAsync663 G
(66G H
u66H I
=>66J L
u66M N
.66N O
Email66O T
==66U W
user66X \
.66\ ]
Email66] b
,66b c
cancellationToken66d u
)66u v
;66v w
if77 
(77 
existingUser77 
!=77 
null77  $
)77$ %
{88 
return99 
Result99 
<99 
Guid99 "
>99" #
.99# $
Failure99$ +
(99+ ,
$str99, B
)99B C
;99C D
}:: 
context<< 
.<< 
Users<< 
.<< 
Add<< 
(<< 
user<< "
)<<" #
;<<# $
await== 
context== 
.== 
SaveChangesAsync== *
(==* +
cancellationToken==+ <
)==< =
;=== >
return>> 
Result>> 
<>> 
Guid>> 
>>> 
.>>  
Success>>  '
(>>' (
user>>( ,
.>>, -
UserId>>- 3
)>>3 4
;>>4 5
}?? 	
}@@ 
}AA 