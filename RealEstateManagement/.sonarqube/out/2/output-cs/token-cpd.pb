‰
XC:\Users\cezar\desktop\RealEstateManagement\Application\ValidationExceptionMiddleware.cs
	namespace 	
Application
 
{ 
public 

class )
ValidationExceptionMiddleware .
{ 
private		 
readonly		 
RequestDelegate		 (
_next		) .
;		. /
public )
ValidationExceptionMiddleware ,
(, -
RequestDelegate- <
next= A
)A B
{ 	
_next 
= 
next 
; 
} 	
public 
async 
Task 
InvokeAsync %
(% &
HttpContext& 1
context2 9
)9 :
{ 	
try 
{ 
await 
_next 
( 
context #
)# $
;$ %
} 
catch 
( 
ValidationException &
ex' )
)) *
{ 
await *
HandleValidationExceptionAsync 4
(4 5
context5 <
,< =
ex> @
)@ A
;A B
} 
} 	
private 
static 
Task *
HandleValidationExceptionAsync :
(: ;
HttpContext; F
contextG N
,N O
ValidationExceptionP c
	exceptiond m
)m n
{ 	
context 
. 
Response 
. 
ContentType (
=) *
$str+ =
;= >
context 
. 
Response 
. 

StatusCode '
=( )
(* +
int+ .
). /
HttpStatusCode/ =
.= >

BadRequest> H
;H I
var!! 
errors!! 
=!! 
	exception!! "
.!!" #
Errors!!# )
."" 
GroupBy"" 
("" 
e"" 
=>"" 
e"" 
."" 
PropertyName"" (
)""( )
.## 
ToDictionary## 
(## 
g## 
=>## 
g##  
.##  !
Key##! $
,##$ %
g##& '
=>##( *
g##+ ,
.##, -
First##- 2
(##2 3
)##3 4
.##4 5
ErrorMessage##5 A
)##A B
;##B C
var%% 
result%% 
=%% 
JsonSerializer%% '
.%%' (
	Serialize%%( 1
(%%1 2
new%%2 5
{%%6 7
errors%%8 >
}%%? @
)%%@ A
;%%A B
return&& 
context&& 
.&& 
Response&& #
.&&# $

WriteAsync&&$ .
(&&. /
result&&/ 5
)&&5 6
;&&6 7
}'' 	
}(( 
})) Í
MC:\Users\cezar\desktop\RealEstateManagement\Application\ValidationBehavior.cs
	namespace 	
Application
 
{ 
public 

class 
ValidationBehavior #
<# $
TRequest$ ,
,, -
	TResponse. 7
>7 8
:9 :
IPipelineBehavior; L
<L M
TRequestM U
,U V
	TResponseW `
>` a
whereb g
TRequesth p
:q r
IRequests {
<{ |
	TResponse	| Ö
>
Ö Ü
{ 
private 
readonly 
IEnumerable $
<$ %

IValidator% /
</ 0
TRequest0 8
>8 9
>9 :

validators; E
;E F
public

 
ValidationBehavior

 !
(

! "
IEnumerable

" -
<

- .

IValidator

. 8
<

8 9
TRequest

9 A
>

A B
>

B C

validators

D N
)

N O
{ 	
this 
. 

validators 
= 

validators (
;( )
} 	
public 
async 
Task 
< 
	TResponse #
># $
Handle% +
(+ ,
TRequest, 4
request5 <
,< ="
RequestHandlerDelegate> T
<T U
	TResponseU ^
>^ _
next` d
,d e
CancellationTokenf w
cancellationToken	x â
)
â ä
{ 	
var 
context 
= 
new 
ValidationContext /
</ 0
TRequest0 8
>8 9
(9 :
request: A
)A B
;B C
var 
failures 
= 

validators %
. 
Select 
( 
v 
=> 
v 
. 
Validate '
(' (
context( /
)/ 0
)0 1
. 

SelectMany 
( 
result "
=># %
result& ,
., -
Errors- 3
)3 4
. 
Where 
( 
f 
=> 
f 
!=  
null! %
)% &
. 
ToList 
( 
) 
; 
if 
( 
failures 
. 
Count 
!=  
$num! "
)" #
{ 
throw 
new 
ValidationException -
(- .
failures. 6
)6 7
;7 8
} 
return 
await 
next 
( 
) 
;  
} 	
} 
} „
LC:\Users\cezar\desktop\RealEstateManagement\Application\utils\PagedResult.cs
	namespace 	
Application
 
. 
Utils 
{ 
public 

class 
PagedResult 
< 
T 
> 
{ 
public 
PagedResult 
( 
List 
<  
T  !
>! "
data# '
,' (
int) ,

totalCount- 7
)7 8
{ 	
Data 
= 
data 
; 

TotalCount 
= 

totalCount #
;# $
}		 	
public 
List 
< 
T 
> 
Data 
{ 
get !
;! "
}# $
public 
int 

TotalCount 
{ 
get  #
;# $
}% &
} 
} ì
OC:\Users\cezar\desktop\RealEstateManagement\Application\utils\MappingProfile.cs
	namespace 	
Application
 
. 
utils 
{ 
public 

class 
MappingProfile 
:  
Profile! (
{ 
public 
MappingProfile 
( 
) 
{ 	
	CreateMap 
< 
PropertyListing %
,% &
PropertyListingDto' 9
>9 :
(: ;
); <
.< =

ReverseMap= G
(G H
)H I
;I J
	CreateMap 
< 
ClientInquiry #
,# $
ClientInquiryDto% 5
>5 6
(6 7
)7 8
.8 9

ReverseMap9 C
(C D
)D E
;E F
	CreateMap 
< 
Transaction !
,! "
TransactionDto# 1
>1 2
(2 3
)3 4
.4 5

ReverseMap5 ?
(? @
)@ A
;A B
	CreateMap 
< 
User 
, 
UserDto #
># $
($ %
)% &
.& '

ReverseMap' 1
(1 2
)2 3
;3 4
	CreateMap 
< (
CreatePropertyListingCommand 2
,2 3
PropertyListing4 C
>C D
(D E
)E F
.F G

ReverseMapG Q
(Q R
)R S
;S T
	CreateMap 
< (
UpdatePropertyListingCommand 2
,2 3
PropertyListing4 C
>C D
(D E
)E F
.F G

ReverseMapG Q
(Q R
)R S
;S T
	CreateMap 
< &
CreateClientInquiryCommand 0
,0 1
ClientInquiry2 ?
>? @
(@ A
)A B
.B C

ReverseMapC M
(M N
)N O
;O P
	CreateMap 
< &
UpdateClientInquiryCommand 0
,0 1
ClientInquiry2 ?
>? @
(@ A
)A B
.B C

ReverseMapC M
(M N
)N O
;O P
	CreateMap 
< $
CreateTransactionCommand .
,. /
Transaction0 ;
>; <
(< =
)= >
.> ?

ReverseMap? I
(I J
)J K
;K L
	CreateMap 
< $
UpdateTransactionCommand .
,. /
Transaction0 ;
>; <
(< =
)= >
.> ?

ReverseMap? I
(I J
)J K
;K L
	CreateMap 
< 
UpdateUserCommand '
,' (
User) -
>- .
(. /
)/ 0
.0 1

ReverseMap1 ;
(; <
)< =
;= >
}   	
}!! 
}"" ˜
pC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\QueryHandlers\GetUserByIdQueryHandler.cs
	namespace		 	
Application		
 
.		 
	Use_Cases		 
.		  
Users		  %
.		% &
QueryHandlers		& 3
{

 
public 

class #
GetUserByIdQueryHandler (
:) *
IRequestHandler+ :
<: ;
GetUserByIdQuery; K
,K L
ResultM S
<S T
UserDtoT [
>[ \
>\ ]
{ 
private 
readonly 
IUserRepository (

repository) 3
;3 4
private 
readonly 
IMapper  
mapper! '
;' (
public #
GetUserByIdQueryHandler &
(& '
IUserRepository' 6

repository7 A
,A B
IMapperC J
mapperK Q
)Q R
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
UserDto! (
>( )
>) *
Handle+ 1
(1 2
GetUserByIdQuery2 B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
{ 	
var 
result 
= 
await 

repository )
.) *
GetUserByIdAsync* :
(: ;
request; B
.B C
UserIdC I
)I J
;J K
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
userDto 
= 
mapper $
.$ %
Map% (
<( )
UserDto) 0
>0 1
(1 2
result2 8
.8 9
Data9 =
)= >
;> ?
return 
Result 
< 
UserDto %
>% &
.& '
Success' .
(. /
userDto/ 6
)6 7
;7 8
} 
else 
{ 
return   
Result   
<   
UserDto   %
>  % &
.  & '
Failure  ' .
(  . /
$"  / 1
$str  1 >
{  > ?
request  ? F
.  F G
UserId  G M
}  M N
$str  N Y
"  Y Z
)  Z [
;  [ \
}!! 
}$$ 	
}%% 
}&& €
pC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\QueryHandlers\GetAllUsersQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Users  %
.% &
QueryHandlers& 3
{		 
public

 

class

 #
GetAllUsersQueryHandler

 (
:

) *
IRequestHandler

+ :
<

: ;
GetAllUsersQuery

; K
,

K L
Result

M S
<

S T
List

T X
<

X Y
UserDto

Y `
>

` a
>

a b
>

b c
{ 
private 
readonly 
IUserRepository (

repository) 3
;3 4
private 
readonly 
IMapper  
mapper! '
;' (
public #
GetAllUsersQueryHandler &
(& '
IUserRepository' 6

repository7 A
,A B
IMapperC J
mapperK Q
)Q R
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &
UserDto& -
>- .
>. /
>/ 0
Handle1 7
(7 8
GetAllUsersQuery8 H
requestI P
,P Q
CancellationTokenR c
cancellationTokend u
)u v
{ 	
var 
result 
= 
await 

repository )
.) *
GetAllUsersAsync* :
(: ;
); <
;< =
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
userDtos 
= 
result %
.% &
Data& *
.* +
Select+ 1
(1 2
user2 6
=>7 9
mapper: @
.@ A
MapA D
<D E
UserDtoE L
>L M
(M N
userN R
)R S
)S T
.T U
ToListU [
([ \
)\ ]
;] ^
return 
Result 
< 
List "
<" #
UserDto# *
>* +
>+ ,
., -
Success- 4
(4 5
userDtos5 =
)= >
;> ?
} 
else 
{ 
return 
Result 
< 
List "
<" #
UserDto# *
>* +
>+ ,
., -
Failure- 4
(4 5
result5 ;
.; <
ErrorMessage< H
)H I
;I J
}   
}!! 	
}"" 
}## û
cC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\Queries\GetUserByIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Users  %
.% &
Queries& -
{ 
public 

class 
GetUserByIdQuery !
:" #
IRequest$ ,
<, -
Result- 3
<3 4
UserDto4 ;
>; <
>< =
{ 
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
}		 
}

 ∂
cC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\Queries\GetAllUsersQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Users  %
.% &
Queries& -
{ 
public 

class 
GetAllUsersQuery !
:" #
IRequest$ ,
<, -
Result- 3
<3 4
List4 8
<8 9
UserDto9 @
>@ A
>A B
>B C
{ 
}		 
}

 ã
nC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\Commands\UpdateUserCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Users  %
.% &
Commands& .
{ 
public 

class &
UpdateUserCommandValidator +
:, -
AbstractValidator. ?
<? @
UpdateUserCommand@ Q
>Q R
{ 
public &
UpdateUserCommandValidator )
() *
)* +
{ 	
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
UserId		 !
)		! "
.		" #
NotEmpty		# +
(		+ ,
)		, -
.		- .
Must		. 2
(		2 3
BeAValidGuid		3 ?
)		? @
;		@ A
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Name

 
)

  
.

  !
MaximumLength

! .
(

. /
$num

/ 1
)

1 2
;

2 3
RuleFor 
( 
x 
=> 
x 
. 
Email  
)  !
.! "
EmailAddress" .
(. /
)/ 0
;0 1
RuleFor 
( 
x 
=> 
x 
. 
PhoneNumber &
)& '
.' (
MaximumLength( 5
(5 6
$num6 8
)8 9
;9 :
} 	
private 
static 
bool 
BeAValidGuid (
(( )
Guid) -
guid. 2
)2 3
{ 	
return 
Guid 
. 
TryParse  
(  !
guid! %
.% &
ToString& .
(. /
)/ 0
,0 1
out2 5
_6 7
)7 8
;8 9
} 	
} 
} «

eC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\Commands\UpdateUserCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Users  %
.% &
Commands& .
{ 
public 

class 
UpdateUserCommand "
:# $
IRequest% -
<- .
Result. 4
<4 5
Guid5 9
>9 :
>: ;
{ 
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
public		 
string		 
?		 
Name		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
string

 
?

 
Email

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 
string 
? 
PhoneNumber "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
? 
Password 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} ü
eC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\Commands\DeleteUserCommand.cs
	namespace		 	
Application		
 
.		 
	Use_Cases		 
.		  
Users		  %
.		% &
Commands		& .
{

 
public 

class 
DeleteUserCommand "
:# $
IRequest% -
<- .
Result. 4
<4 5
Guid5 9
>9 :
>: ;
{ 
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
} 
} ›
sC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\CommandHandlers\UpdateUserCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Users  %
.% &
CommandHandlers& 5
{		 
public

 

class

 $
UpdateUserCommandHandler

 *
:

+ ,
IRequestHandler

- <
<

< =
UpdateUserCommand

= N
,

N O
Result

P V
<

V W
Guid

W [
>

[ \
>

\ ]
{ 
private 
readonly 
IUserRepository (

repository) 3
;3 4
private 
readonly 
IMapper  
mapper! '
;' (
public $
UpdateUserCommandHandler '
(' (
IUserRepository( 7

repository8 B
,B C
IMapperD K
mapperL R
)R S
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /
UpdateUserCommand/ @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 	
var 
user 
= 
mapper 
. 
Map !
<! "
User" &
>& '
(' (
request( /
)/ 0
;0 1
if 
( 
! 
string 
. 
IsNullOrEmpty %
(% &
request& -
.- .
Password. 6
)6 7
)7 8
{ 
user 
. 
PasswordHash !
=" #
BCrypt$ *
.* +
Net+ .
.. /
BCrypt/ 5
.5 6
HashPassword6 B
(B C
requestC J
.J K
PasswordK S
)S T
;T U
} 
var 
result 
= 
await 

repository )
.) *
UpdateUserAsync* 9
(9 :
user: >
)> ?
;? @
if 
( 
result 
. 
	IsSuccess  
)  !
{   
return!! 
Result!! 
<!! 
Guid!! "
>!!" #
.!!# $
Success!!$ +
(!!+ ,
result!!, 2
.!!2 3
Data!!3 7
)!!7 8
;!!8 9
}"" 
return## 
Result## 
<## 
Guid## 
>## 
.##  
Failure##  '
(##' (
result##( .
.##. /
ErrorMessage##/ ;
)##; <
;##< =
}$$ 	
}&& 
}'' á
sC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Users\CommandHandlers\DeleteUserCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Users  %
.% &
CommandHandlers& 5
{ 
public		 

class		 $
DeleteUserCommandHandler		 )
:		* +
IRequestHandler		, ;
<		; <
DeleteUserCommand		< M
,		M N
Result		O U
<		U V
Guid		V Z
>		Z [
>		[ \
{

 
private 
readonly 
IUserRepository (

repository) 3
;3 4
public $
DeleteUserCommandHandler '
(' (
IUserRepository( 7

repository8 B
)B C
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /
DeleteUserCommand/ @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 	
var 
result 
= 
await 

repository )
.) *
DeleteUserAsync* 9
(9 :
request: A
.A B
UserIdB H
)H I
;I J
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
} ∂
xC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Queries\GetTransactionsBySellerIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
Queries- 4
{ 
public 

class *
GetTransactionsBySellerIdQuery /
:0 1
IRequest2 :
<: ;
Result; A
<A B
PagedResultB M
<M N
TransactionDtoN \
>\ ]
>] ^
>^ _
{		 
public

 
Guid

 
SellerId

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 
int 
Page 
{ 
get 
; 
set "
;" #
}$ %
public 
int 
PageSize 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ≥
wC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Queries\GetTransactionsByBuyerIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
Queries- 4
{ 
public 

class )
GetTransactionsByBuyerIdQuery .
:/ 0
IRequest1 9
<9 :
Result: @
<@ A
PagedResultA L
<L M
TransactionDtoM [
>[ \
>\ ]
>] ^
{		 
public

 
Guid

 
BuyerId

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
public 
int 
Page 
{ 
get 
; 
set "
;" #
}$ %
public 
int 
PageSize 
{ 
get !
;! "
set# &
;& '
}( )
} 
} É
yC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Queries\GetTransactionByPropertyIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
Queries- 4
{ 
public 

class +
GetTransactionByPropertyIdQuery 0
:1 2
IRequest3 ;
<; <
Result< B
<B C
TransactionDtoC Q
>Q R
>R S
{ 
public		 
Guid		 

PropertyId		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
public

 
int

 
Page

 
{

 
get

 
;

 
set

 "
;

" #
}

$ %
public 
int 
PageSize 
{ 
get !
;! "
set# &
;& '
}( )
} 
} »
qC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Queries\GetTransactionByIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
Queries- 4
{ 
public 

class #
GetTransactionByIdQuery (
:) *
IRequest+ 3
<3 4
Result4 :
<: ;
TransactionDto; I
>I J
>J K
{ 
public		 
Guid		 
TransactionId		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
}

 
} Ÿ
qC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Queries\GetAllTransactionsQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
Queries- 4
{ 
public 

class #
GetAllTransactionsQuery (
:) *
IRequest+ 3
<3 4
Result4 :
<: ;
List; ?
<? @
TransactionDto@ N
>N O
>O P
>P Q
{ 
}		 
}

 £
àC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\QueriesHandlers\GetTransactionByPropertyIdQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
QueriesHandlers- <
{		 
public

 

class

 2
&GetTransactionByPropertyIdQueryHandler

 7
:

8 9
IRequestHandler

: I
<

I J+
GetTransactionByPropertyIdQuery

J i
,

i j
Result

k q
<

q r
TransactionDto	

r Ä
>


Ä Å
>


Å Ç
{ 
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly "
ITransactionRepository /

repository0 :
;: ;
public 2
&GetTransactionByPropertyIdQueryHandler 5
(5 6
IMapper6 =
mapper> D
,D E"
ITransactionRepositoryF \

repository] g
)g h
{ 	
this 
. 
mapper 
= 
mapper  
;  !
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
TransactionDto! /
>/ 0
>0 1
Handle2 8
(8 9+
GetTransactionByPropertyIdQuery9 X
requestY `
,` a
CancellationTokenb s
cancellationToken	t Ö
)
Ö Ü
{ 	
var 
result 
= 
await 

repository )
.) *+
GetTransactionByPropertyIdAsync* I
(I J
requestJ Q
.Q R

PropertyIdR \
)\ ]
;] ^
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
transactionDto "
=# $
mapper% +
.+ ,
Map, /
</ 0
TransactionDto0 >
>> ?
(? @
result@ F
.F G
DataG K
)K L
;L M
return 
Result 
< 
TransactionDto ,
>, -
.- .
Success. 5
(5 6
transactionDto6 D
)D E
;E F
} 
return 
Result 
< 
TransactionDto (
>( )
.) *
Failure* 1
(1 2
result2 8
.8 9
ErrorMessage9 E
)E F
;F G
}   	
}!! 
}"" Å
ÄC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\QueriesHandlers\GetTransactionByIdQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
QueriesHandlers- <
{		 
public

 

class

 *
GetTransactionByIdQueryHandler

 /
:

0 1
IRequestHandler

2 A
<

A B#
GetTransactionByIdQuery

B Y
,

Y Z
Result

[ a
<

a b
TransactionDto

b p
>

p q
>

q r
{ 
private 
readonly "
ITransactionRepository /

repository0 :
;: ;
private 
readonly 
IMapper  
mapper! '
;' (
public *
GetTransactionByIdQueryHandler -
(- ."
ITransactionRepository. D

repositoryE O
,O P
IMapperQ X
mapperY _
)_ `
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
TransactionDto! /
>/ 0
>0 1
Handle2 8
(8 9#
GetTransactionByIdQuery9 P
requestQ X
,X Y
CancellationTokenZ k
cancellationTokenl }
)} ~
{ 	
var 
result 
= 
await 

repository )
.) *#
GetTransactionByIdAsync* A
(A B
requestB I
.I J
TransactionIdJ W
)W X
;X Y
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
transactionDto "
=# $
mapper% +
.+ ,
Map, /
</ 0
TransactionDto0 >
>> ?
(? @
result@ F
.F G
DataG K
)K L
;L M
return 
Result 
< 
TransactionDto ,
>, -
.- .
Success. 5
(5 6
transactionDto6 D
)D E
;E F
} 
else 
{ 
return 
Result 
< 
TransactionDto ,
>, -
.- .
Failure. 5
(5 6
$"6 8
$str8 L
{L M
requestM T
.T U
TransactionIdU b
}b c
$strc n
"n o
)o p
;p q
}   
}## 	
}%% 
}&& Ë
ÄC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\QueriesHandlers\GetAllTransactionsQueryHandler.cs
	namespace		 	
Application		
 
.		 
	Use_Cases		 
.		  
Transactions		  ,
.		, -
QueriesHandlers		- <
{

 
public 

class *
GetAllTransactionsQueryHandler /
:0 1
IRequestHandler2 A
<A B#
GetAllTransactionsQueryB Y
,Y Z
Result[ a
<a b
Listb f
<f g
TransactionDtog u
>u v
>v w
>w x
{ 
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly "
ITransactionRepository /

repository0 :
;: ;
public *
GetAllTransactionsQueryHandler -
(- .
IMapper. 5
mapper6 <
,< ="
ITransactionRepository> T

repositoryU _
)_ `
{ 	
this 
. 
mapper 
= 
mapper 
;  
this 
. 

repository 
= 

repository '
;' (
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &
TransactionDto& 4
>4 5
>5 6
>6 7
Handle8 >
(> ?#
GetAllTransactionsQuery? V
requestW ^
,^ _
CancellationToken` q
cancellationToken	r É
)
É Ñ
{ 	
var 
result 
= 
await 

repository )
.) *#
GetAllTransactionsAsync* A
(A B
)B C
;C D
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
transactionDtos #
=$ %
result& ,
., -
Data- 1
.1 2
Select2 8
(8 9
transaction9 D
=>E G
mapperH N
.N O
MapO R
<R S
TransactionDtoS a
>a b
(b c
transactionc n
)n o
)o p
.p q
ToListq w
(w x
)x y
;y z
return 
Result 
< 
List "
<" #
TransactionDto# 1
>1 2
>2 3
.3 4
Success4 ;
(; <
transactionDtos< K
)K L
;L M
} 
else 
{ 
return   
Result   
<   
List   "
<  " #
TransactionDto  # 1
>  1 2
>  2 3
.  3 4
Failure  4 ;
(  ; <
result  < B
.  B C
ErrorMessage  C O
)  O P
;  P Q
}!! 
}"" 	
}## 
}$$ À
|C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Commands\UpdateTransactionCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
Commands- 5
{ 
public 

class -
!UpdateTransactionCommandValidator 2
:3 4
AbstractValidator5 F
<F G$
UpdateTransactionCommandG _
>_ `
{ 
public -
!UpdateTransactionCommandValidator 0
(0 1
)1 2
{		 	
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
TransactionId

 (
)

( )
.

) *
NotEmpty

* 2
(

2 3
)

3 4
.

4 5
Must

5 9
(

9 :
BeAValidGuid

: F
)

F G
;

G H
RuleFor 
( 
x 
=> 
x 
. 

PropertyId %
)% &
.& '
NotEmpty' /
(/ 0
)0 1
.1 2
Must2 6
(6 7
BeAValidGuid7 C
)C D
;D E
RuleFor 
( 
x 
=> 
x 
. 
BuyerId "
)" #
.# $
NotEmpty$ ,
(, -
)- .
.. /
Must/ 3
(3 4
BeAValidGuid4 @
)@ A
;A B
RuleFor 
( 
x 
=> 
x 
. 
SellerId #
)# $
.$ %
NotEmpty% -
(- .
). /
./ 0
Must0 4
(4 5
BeAValidGuid5 A
)A B
;B C
RuleFor 
( 
x 
=> 
x 
. 
	SalePrice $
)$ %
.% &
NotEmpty& .
(. /
)/ 0
.0 1
GreaterThan1 <
(< =
$num= >
)> ?
;? @
} 	
private 
static 
bool 
BeAValidGuid (
(( )
Guid) -
guid. 2
)2 3
{ 	
return 
Guid 
. 
TryParse  
(  !
guid! %
.% &
ToString& .
(. /
)/ 0
,0 1
out2 5
_6 7
)7 8
;8 9
} 	
} 
} π

sC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Commands\UpdateTransactionCommand.cs
	namespace 	 
RealEstateManagement
 
. 
Application *
.* +
Transactions+ 7
.7 8
Commands8 @
{ 
public 

class $
UpdateTransactionCommand )
:* +
IRequest, 4
<4 5
Result5 ;
<; <
Guid< @
>@ A
>A B
{ 
public		 
Guid		 
TransactionId		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
public

 
Guid

 

PropertyId

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
Guid 
BuyerId 
{ 
get !
;! "
set# &
;& '
}( )
public 
Guid 
SellerId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
double 
	SalePrice 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} Õ
sC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Commands\DeleteTransactionCommand.cs
	namespace 	 
RealEstateManagement
 
. 
Application *
.* +
Transactions+ 7
.7 8
Commands8 @
{ 
public 

class $
DeleteTransactionCommand )
:* +
IRequest, 4
<4 5
Result5 ;
<; <
Guid< @
>@ A
>A B
{ 
public		 
Guid		 
TransactionId		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
}

 
} ï
|C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Commands\CreateTransactionCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
Commands- 5
{ 
public 

class -
!CreateTransactionCommandValidator 2
:3 4
AbstractValidator5 F
<F G$
CreateTransactionCommandG _
>_ `
{ 
public -
!CreateTransactionCommandValidator 0
(0 1
)1 2
{ 	
RuleFor		 
(		 
x		 
=>		 
x		 
.		 

PropertyId		 #
)		# $
.		$ %
NotEmpty		% -
(		- .
)		. /
.		/ 0
Must		0 4
(		4 5
BeAValidGuid		5 A
)		A B
;		B C
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
BuyerId

 "
)

" #
.

# $
NotEmpty

$ ,
(

, -
)

- .
.

. /
Must

/ 3
(

3 4
BeAValidGuid

4 @
)

@ A
;

A B
RuleFor 
( 
x 
=> 
x 
. 
SellerId #
)# $
.$ %
NotEmpty% -
(- .
). /
./ 0
Must0 4
(4 5
BeAValidGuid5 A
)A B
;B C
RuleFor 
( 
x 
=> 
x 
. 
	SalePrice $
)$ %
.% &
NotEmpty& .
(. /
)/ 0
.0 1
GreaterThan1 <
(< =
$num= >
)> ?
;? @
} 	
private 
static 
bool 
BeAValidGuid (
(( )
Guid) -
guid. 2
)2 3
{ 	
return 
Guid 
. 
TryParse  
(  !
guid! %
.% &
ToString& .
(. /
)/ 0
,0 1
out2 5
_6 7
)7 8
;8 9
} 	
} 
} è	
sC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\Commands\CreateTransactionCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
Commands- 5
{ 
public 

class $
CreateTransactionCommand )
:* +
IRequest, 4
<4 5
Result5 ;
<; <
Guid< @
>@ A
>A B
{ 
public 
Guid 

PropertyId 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
Guid		 
BuyerId		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
Guid

 
SellerId

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 
double 
	SalePrice 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} ó
ÅC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\CommandHandlers\UpdateTransactionCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
CommandHandlers- <
{		 
public

 

class

 +
UpdateTransactionCommandHandler

 0
:

1 2
IRequestHandler

3 B
<

B C$
UpdateTransactionCommand

C [
,

[ \
Result

] c
<

c d
Guid

d h
>

h i
>

i j
{ 
private 
readonly "
ITransactionRepository /

repository0 :
;: ;
private 
readonly 
IMapper  
mapper! '
;' (
public +
UpdateTransactionCommandHandler .
(. /"
ITransactionRepository/ E

repositoryF P
,P Q
IMapperR Y
mapperZ `
)` a
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /$
UpdateTransactionCommand/ G
requestH O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
{ 	
var 
transaction 
= 
mapper $
.$ %
Map% (
<( )
Transaction) 4
>4 5
(5 6
request6 =
)= >
;> ?
var 
result 
= 
await 

repository )
.) *"
UpdateTransactionAsync* @
(@ A
transactionA L
)L M
;M N
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
}   ‡
ÅC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\CommandHandlers\DeleteTransactionCommandHandler.cs
	namespace 	 
RealEstateManagement
 
. 
Application *
.* +
Transactions+ 7
.7 8
CommandHandlers8 G
{ 
public 

class +
DeleteTransactionCommandHandler 0
:1 2
IRequestHandler3 B
<B C$
DeleteTransactionCommandC [
,[ \
Result] c
<c d
Guidd h
>h i
>i j
{		 
private

 
readonly

 "
ITransactionRepository

 /

repository

0 :
;

: ;
public +
DeleteTransactionCommandHandler .
(. /"
ITransactionRepository/ E

repositoryF P
)P Q
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /$
DeleteTransactionCommand/ G
requestH O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
{ 	
var 
result 
= 
await 

repository )
.) *"
DeleteTransactionAsync* @
(@ A
requestA H
.H I
TransactionIdI V
)V W
;W X
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
} ±
ÉC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\QueryHandlers\GetListingsByUserIdQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
PropertyListings  0
.0 1
QueryHandlers1 >
{		 
public

 

class

 +
GetListingsByUserIdQueryHandler

 0
:

1 2
IRequestHandler

3 B
<

B C$
GetListingsByUserIdQuery

C [
,

[ \
Result

] c
<

c d
List

d h
<

h i
PropertyListingDto

i {
>

{ |
>

| }
>

} ~
{ 
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly &
IPropertyListingRepository 3

repository4 >
;> ?
public +
GetListingsByUserIdQueryHandler .
(. /
IMapper/ 6
mapper7 =
,= >&
IPropertyListingRepository? Y

repositoryZ d
)d e
{ 	
this 
. 
mapper 
= 
mapper  
;  !
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &
PropertyListingDto& 8
>8 9
>9 :
>: ;
Handle< B
(B C$
GetListingsByUserIdQueryC [
request\ c
,c d
CancellationTokene v
cancellationToken	w à
)
à â
{ 	
var 
result 
= 
await 

repository )
.) *
GetListingsByUserId* =
(= >
request> E
.E F
UserIdF L
)L M
;M N
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
listingsDto 
=  !
result" (
.( )
Data) -
.- .
Select. 4
(4 5
listing5 <
=>= ?
mapper@ F
.F G
MapG J
<J K
PropertyListingDtoK ]
>] ^
(^ _
listing_ f
)f g
)g h
.h i
ToListi o
(o p
)p q
;q r
return 
Result 
< 
List "
<" #
PropertyListingDto# 5
>5 6
>6 7
.7 8
Success8 ?
(? @
listingsDto@ K
)K L
;L M
} 
else 
{ 
return 
Result 
< 
List "
<" #
PropertyListingDto# 5
>5 6
>6 7
.7 8
Failure8 ?
(? @
result@ F
.F G
ErrorMessageG S
)S T
;T U
}   
}!! 	
}"" 
}## î
ÅC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Transactions\CommandHandlers\CreateTransactionCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Transactions  ,
., -
CommandHandlers- <
{		 
public

 

class

 +
CreateTransactionCommandHandler

 0
:

1 2
IRequestHandler

3 B
<

B C$
CreateTransactionCommand

C [
,

[ \
Result

] c
<

c d
Guid

d h
>

h i
>

i j
{ 
private 
readonly "
ITransactionRepository /

repository0 :
;: ;
private 
readonly 
IMapper  
mapper! '
;' (
public +
CreateTransactionCommandHandler .
(. /"
ITransactionRepository/ E

repositoryF P
,P Q
IMapperR Y
mapperZ `
)` a
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /$
CreateTransactionCommand/ G
requestH O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
{ 	
var 
transaction 
= 
mapper $
.$ %
Map% (
<( )
Transaction) 4
>4 5
(5 6
request6 =
)= >
;> ?
var 
result 
= 
await 

repository )
.) *
AddTransactionAsync* =
(= >
transaction> I
)I J
;J K
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
}   Ì
~C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\QueryHandlers\GetListingByIdQueryHandler.cs
	namespace		 	
Application		
 
.		 
	Use_Cases		 
.		  
QueryHandlers		  -
{

 
public 

class &
GetListingByIdQueryHandler +
:, -
IRequestHandler. =
<= >
GetListingByIdQuery> Q
,Q R
ResultS Y
<Y Z
PropertyListingDtoZ l
>l m
>m n
{ 
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly &
IPropertyListingRepository 3

repository4 >
;> ?
public &
GetListingByIdQueryHandler )
() *
IMapper* 1
mapper2 8
,8 9&
IPropertyListingRepository: T

repositoryU _
)_ `
{ 	
this 
. 
mapper 
= 
mapper  
;  !
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
PropertyListingDto! 3
>3 4
>4 5
Handle6 <
(< =
GetListingByIdQuery= P
requestQ X
,X Y
CancellationTokenZ k
cancellationTokenl }
)} ~
{ 	
var 
result 
= 
await 

repository )
.) *
GetListingByIdAsync* =
(= >
request> E
.E F

PropertyIdF P
)P Q
;Q R
if 
( 
result 
. 
	IsSuccess !
)! "
{ 
var 

listingDto 
=  
mapper! '
.' (
Map( +
<+ ,
PropertyListingDto, >
>> ?
(? @
result@ F
.F G
DataG K
)K L
;L M
return 
Result 
< 
PropertyListingDto 0
>0 1
.1 2
Success2 9
(9 :

listingDto: D
)D E
;E F
} 
else 
{ 
return   
Result   
<   
PropertyListingDto   0
>  0 1
.  1 2
Failure  2 9
(  9 :
result  : @
.  @ A
ErrorMessage  A M
)  M N
;  N O
}!! 
}"" 	
}## 
}$$ ´&
ãC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\QueryHandlers\GetFilteredPropertyListingsQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
PropertyListings  0
.0 1
QueryHandlers1 >
{ 
public 

class 3
'GetFilteredPropertyListingsQueryHandler 8
:9 :
IRequestHandler; J
<J K,
 GetFilteredPropertyListingsQueryK k
,k l
Resultm s
<s t
PagedResultt 
<	 Ä 
PropertyListingDto
Ä í
>
í ì
>
ì î
>
î ï
{ 
private 
readonly &
IPropertyListingRepository 3

repository4 >
;> ?
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly 
IEnumerable $
<$ %*
IPropertyListingFilterStrategy% C
>C D
filterStrategiesE U
;U V
public 3
'GetFilteredPropertyListingsQueryHandler 6
(6 7&
IPropertyListingRepository7 Q

repositoryR \
,\ ]
IMapper^ e
mapperf l
,l m
IEnumerablen y
<y z+
IPropertyListingFilterStrategy	z ò
>
ò ô
filterStrategies
ö ™
)
™ ´
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
this 
. 
filterStrategies !
=" #
filterStrategies$ 4
;4 5
} 	
public 
async 
Task 
< 
Result  
<  !
PagedResult! ,
<, -
PropertyListingDto- ?
>? @
>@ A
>A B
HandleC I
(I J,
 GetFilteredPropertyListingsQueryJ j
requestk r
,r s
CancellationToken	t Ö
cancellationToken
Ü ó
)
ó ò
{ 	
var "
propertyListingsResult &
=' (
await) .

repository/ 9
.9 :
GetAllListingsAsync: M
(M N
)N O
;O P
if 
( 
! "
propertyListingsResult '
.' (
	IsSuccess( 1
)1 2
{   
return!! 
Result!! 
<!! 
PagedResult!! )
<!!) *
PropertyListingDto!!* <
>!!< =
>!!= >
.!!> ?
Failure!!? F
(!!F G"
propertyListingsResult!!G ]
.!!] ^
ErrorMessage!!^ j
)!!j k
;!!k l
}"" 
var$$ 
propertyListings$$  
=$$! ""
propertyListingsResult$$# 9
.$$9 :
Data$$: >
;$$> ?
var%% 
query%% 
=%% 
propertyListings%% (
.%%( )
AsQueryable%%) 4
(%%4 5
)%%5 6
;%%6 7
foreach(( 
((( 
var(( 
strategy(( !
in((" $
filterStrategies((% 5
)((5 6
{)) 
query** 
=** 
strategy**  
.**  !
ApplyFilter**! ,
(**, -
query**- 2
,**2 3
request**4 ;
)**; <
;**< =
}++ 
var.. !
pagedPropertyListings.. %
=..& '
query..( -
...- .
ApplyPaging... 9
(..9 :
request..: A
...A B
Page..B F
,..F G
request..H O
...O P
PageSize..P X
)..X Y
;..Y Z
var00 
PropertyListingDtos00 #
=00$ %
mapper00& ,
.00, -
Map00- 0
<000 1
List001 5
<005 6
PropertyListingDto006 H
>00H I
>00I J
(00J K!
pagedPropertyListings00K `
)00` a
;00a b
var22 
pagedResult22 
=22 
new22 !
PagedResult22" -
<22- .
PropertyListingDto22. @
>22@ A
(22A B
PropertyListingDtos22B U
,22U V
query22W \
.22\ ]
Count22] b
(22b c
)22c d
)22d e
;22e f
return44 
Result44 
<44 
PagedResult44 %
<44% &
PropertyListingDto44& 8
>448 9
>449 :
.44: ;
Success44; B
(44B C
pagedResult44C N
)44N O
;44O P
}66 	
}77 
}88 ü
ÖC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\QueryHandlers\GetAllPropertyListingQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
QueryHandlers  -
{		 
public

 

class

 -
!GetAllPropertyListingQueryHandler

 2
:

3 4
IRequestHandler

5 D
<

D E&
GetAllPropertyListingQuery

E _
,

_ `
Result

a g
<

g h
List

h l
<

l m
PropertyListingDto

m 
>	

 Ä
>


Ä Å
>


Å Ç
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly &
IPropertyListingRepository 3
_repository4 ?
;? @
public -
!GetAllPropertyListingQueryHandler 0
(0 1
IMapper1 8
mapper9 ?
,? @&
IPropertyListingRepositoryA [

repository\ f
)f g
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &
PropertyListingDto& 8
>8 9
>9 :
>: ;
Handle< B
(B C&
GetAllPropertyListingQueryC ]
request^ e
,e f
CancellationTokeng x
cancellationToken	y ä
)
ä ã
{ 	
var 
result 
= 
await 
_repository *
.* +
GetAllListingsAsync+ >
(> ?
)? @
;@ A
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
listingsDto 
=  !
result" (
.( )
Data) -
.- .
Select. 4
(4 5
listing5 <
=>= ?
_mapper@ G
.G H
MapH K
<K L
PropertyListingDtoL ^
>^ _
(_ `
listing` g
)g h
)h i
.i j
ToListj p
(p q
)q r
;r s
return 
Result 
< 
List "
<" #
PropertyListingDto# 5
>5 6
>6 7
.7 8
Success8 ?
(? @
listingsDto@ K
)K L
;L M
} 
else 
{ 
return 
Result 
< 
List "
<" #
PropertyListingDto# 5
>5 6
>6 7
.7 8
Failure8 ?
(? @
result@ F
.F G
ErrorMessageG S
)S T
;T U
}   
}"" 	
}## 
}$$ ˇ
vC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Queries\GetListingsByUserIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
PropertyListings  0
.0 1
Queries1 8
{ 
public 

class $
GetListingsByUserIdQuery )
:* +
IRequest, 4
<4 5
Result5 ;
<; <
List< @
<@ A
PropertyListingDtoA S
>S T
>T U
>U V
{ 
public		 
Guid		 
UserId		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
}

 
} ú
qC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Queries\GetListingByIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Queries  '
{ 
public 

class 
GetListingByIdQuery $
:% &
IRequest' /
</ 0
Result0 6
<6 7
PropertyListingDto7 I
>I J
>J K
{ 
public		 
Guid		 

PropertyId		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
}

 
} ˇ
~C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Queries\GetFilteredPropertyListingsQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
PropertyListings  0
.0 1
Queries1 8
{ 
public 

class ,
 GetFilteredPropertyListingsQuery 1
:2 3
IRequest4 <
<< =
Result= C
<C D
PagedResultD O
<O P
PropertyListingDtoP b
>b c
>c d
>d e
{		 
public

 
int

 
Page

 
{

 
get

 
;

 
set

 "
;

" #
}

$ %
public 
int 
PageSize 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
? 
Type 
{ 
get !
;! "
set# &
;& '
}( )
public 
double 
Price 
{ 
get !
;! "
set# &
;& '
}( )
public 
double 
SquareFootage #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
double 
NumberOfBedrooms &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBathrooms '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
? 
Status 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} æ
xC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Queries\GetAllPropertyListingQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Queries  '
{ 
public 

class &
GetAllPropertyListingQuery +
:, -
IRequest. 6
<6 7
Result7 =
<= >
List> B
<B C
PropertyListingDtoC U
>U V
>V W
>W X
{ 
}		 
}

 Ô=
rC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Filtering\TypeFilterStrategy.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
PropertyListings  0
.0 1
	Filtering1 :
{ 
public 

class 
TypeFilterStrategy #
:$ %*
IPropertyListingFilterStrategy& D
{ 
public 

IQueryable 
< 
PropertyListing )
>) *
ApplyFilter+ 6
(6 7

IQueryable7 A
<A B
PropertyListingB Q
>Q R
queryS X
,X Y,
 GetFilteredPropertyListingsQueryZ z
request	{ Ç
)
Ç É
{		 	
if

 
(

 
!

 
string

 
.

 
IsNullOrEmpty

 %
(

% &
request

& -
.

- .
Type

. 2
)

2 3
)

3 4
{ 
query 
= 
query 
. 
Where #
(# $
x$ %
=>& (
string) /
./ 0
Equals0 6
(6 7
x7 8
.8 9
Type9 =
.= >
ToLower> E
(E F
)F G
,G H
requestH O
.O P
TypeP T
.T U
ToLowerU \
(\ ]
)] ^
)^ _
)_ `
;` a
} 
return 
query 
; 
} 	
} 
public 

class 
PriceFilterStrategy $
:% &*
IPropertyListingFilterStrategy' E
{ 
readonly 
float 
epsilon 
=  
$num! (
;( )
public 

IQueryable 
< 
PropertyListing )
>) *
ApplyFilter+ 6
(6 7

IQueryable7 A
<A B
PropertyListingB Q
>Q R
queryS X
,X Y,
 GetFilteredPropertyListingsQueryZ z
request	{ Ç
)
Ç É
{ 	
if 
( 
request 
. 
Price 
> 
$num  !
)! "
{ 
query 
= 
query 
. 
Where #
(# $
x$ %
=>& (
Math) -
.- .
Abs. 1
(1 2
x2 3
.3 4
Price4 9
-: ;
request< C
.C D
PriceD I
)I J
<K L
epsilonM T
)T U
;U V
} 
return 
query 
; 
} 	
} 
public!! 

class!! '
SquareFootageFilterStrategy!! ,
:!!- .*
IPropertyListingFilterStrategy!!/ M
{"" 
readonly## 
float## 
epsilon## 
=##  
$num##! (
;##( )
public$$ 

IQueryable$$ 
<$$ 
PropertyListing$$ )
>$$) *
ApplyFilter$$+ 6
($$6 7

IQueryable$$7 A
<$$A B
PropertyListing$$B Q
>$$Q R
query$$S X
,$$X Y,
 GetFilteredPropertyListingsQuery$$Z z
request	$${ Ç
)
$$Ç É
{%% 	
if&& 
(&& 
request&& 
.&& 
SquareFootage&& %
>&&& '
$num&&( )
)&&) *
{'' 
query(( 
=(( 
query(( 
.(( 
Where(( #
(((# $
x(($ %
=>((& (
Math(() -
.((- .
Abs((. 1
(((1 2
x((2 3
.((3 4
SquareFootage((4 A
-((B C
request((D K
.((K L
SquareFootage((L Y
)((Y Z
<(([ \
epsilon((] d
)((d e
;((e f
})) 
return** 
query** 
;** 
}++ 	
},, 
public.. 

class.. *
NumberOfBedroomsFilterStrategy.. /
:..0 1*
IPropertyListingFilterStrategy..2 P
{// 
readonly00 
float00 
epsilon00 
=00  
$num00! (
;00( )
public11 

IQueryable11 
<11 
PropertyListing11 )
>11) *
ApplyFilter11+ 6
(116 7

IQueryable117 A
<11A B
PropertyListing11B Q
>11Q R
query11S X
,11X Y,
 GetFilteredPropertyListingsQuery11Z z
request	11{ Ç
)
11Ç É
{22 	
if33 
(33 
request33 
.33 
NumberOfBedrooms33 (
>33) *
$num33+ ,
)33, -
{44 
query55 
=55 
query55 
.55 
Where55 #
(55# $
x55$ %
=>55& (
Math55) -
.55- .
Abs55. 1
(551 2
x552 3
.553 4
NumberOfBedrooms554 D
-55E F
request55G N
.55N O
NumberOfBedrooms55O _
)55_ `
<55a b
epsilon55c j
)55j k
;55k l
}66 
return77 
query77 
;77 
}88 	
}99 
public;; 

class;; +
NumberOfBathroomsFilterStrategy;; 0
:;;1 2*
IPropertyListingFilterStrategy;;3 Q
{<< 
readonly== 
float== 
epsilon== 
===  
$num==! (
;==( )
public>> 

IQueryable>> 
<>> 
PropertyListing>> )
>>>) *
ApplyFilter>>+ 6
(>>6 7

IQueryable>>7 A
<>>A B
PropertyListing>>B Q
>>>Q R
query>>S X
,>>X Y,
 GetFilteredPropertyListingsQuery>>Z z
request	>>{ Ç
)
>>Ç É
{?? 	
if@@ 
(@@ 
request@@ 
.@@ 
NumberOfBathrooms@@ )
>@@* +
$num@@, -
)@@- .
{AA 
queryBB 
=BB 
queryBB 
.BB 
WhereBB #
(BB# $
xBB$ %
=>BB& (
MathBB) -
.BB- .
AbsBB. 1
(BB1 2
xBB2 3
.BB3 4
NumberOfBathroomsBB4 E
-BBF G
requestBBH O
.BBO P
NumberOfBathroomsBBP a
)BBa b
<BBc d
epsilonBBe l
)BBl m
;BBm n
}CC 
returnDD 
queryDD 
;DD 
}EE 	
}FF 
publicHH 

classHH  
StatusFilterStrategyHH %
:HH& '*
IPropertyListingFilterStrategyHH( F
{II 
publicJJ 

IQueryableJJ 
<JJ 
PropertyListingJJ )
>JJ) *
ApplyFilterJJ+ 6
(JJ6 7

IQueryableJJ7 A
<JJA B
PropertyListingJJB Q
>JJQ R
queryJJS X
,JJX Y,
 GetFilteredPropertyListingsQueryJJZ z
request	JJ{ Ç
)
JJÇ É
{KK 	
ifLL 
(LL 
!LL 
stringLL 
.LL 
IsNullOrEmptyLL %
(LL% &
requestLL& -
.LL- .
StatusLL. 4
)LL4 5
)LL5 6
{MM 
queryNN 
=NN 
queryNN 
.NN 
WhereNN #
(NN# $
xNN$ %
=>NN& (
xNN) *
.NN* +
StatusNN+ 1
==NN2 4
requestNN5 <
.NN< =
StatusNN= C
)NNC D
;NND E
}OO 
returnPP 
queryPP 
;PP 
}QQ 	
}RR 
}SS á
~C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Filtering\IPropertyListingFilterStrategy.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
PropertyListings  0
.0 1
	Filtering1 :
{ 
public 

	interface *
IPropertyListingFilterStrategy 3
{ 

IQueryable		 
<		 
PropertyListing		 "
>		" #
ApplyFilter		$ /
(		/ 0

IQueryable		0 :
<		: ;
PropertyListing		; J
>		J K
query		L Q
,		Q R,
 GetFilteredPropertyListingsQuery		S s
request		t {
)		{ |
;		| }
}

 
} Ó!
ÑC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Commands\UpdatePropertyListingCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class 1
%UpdatePropertyListingCommandValidator 6
:7 8
AbstractValidator9 J
<J K(
UpdatePropertyListingCommandK g
>g h
{ 
public 1
%UpdatePropertyListingCommandValidator 4
(4 5
)5 6
{ 	
RuleFor		 
(		 
x		 
=>		 
x		 
.		 

PropertyId		 %
)		% &
.		& '
NotEmpty		' /
(		/ 0
)		0 1
.		1 2
Must		2 6
(		6 7
BeAValidGuid		7 C
)		C D
;		D E
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Title

  
)

  !
.

! "
MaximumLength

" /
(

/ 0
$num

0 3
)

3 4
;

4 5
RuleFor 
( 
x 
=> 
x 
. 
Address "
)" #
.# $
MaximumLength$ 1
(1 2
$num2 5
)5 6
;6 7
RuleFor 
( 
x 
=> 
x 
. 
Price  
)  !
.! "
GreaterThan" -
(- .
$num. /
)/ 0
;0 1
RuleFor 
( 
x 
=> 
x 
. 
SquareFootage (
)( )
.) *
GreaterThan* 5
(5 6
$num6 7
)7 8
;8 9
RuleFor 
( 
x 
=> 
x 
. 
NumberOfBedrooms +
)+ ,
., - 
GreaterThanOrEqualTo- A
(A B
$numB C
)C D
;D E
RuleFor 
( 
x 
=> 
x 
. 
NumberOfBathrooms ,
), -
.- . 
GreaterThanOrEqualTo. B
(B C
$numC D
)D E
;E F
RuleFor 
( 
x 
=> 
x 
. 
Description &
)& '
.' (
NotEmpty( 0
(0 1
)1 2
.2 3
MaximumLength3 @
(@ A
$numA D
)D E
;E F
RuleFor 
( 
x 
=> 
x 
. 
Status !
)! "
. 
NotEmpty 
( 
) 
. 
Must 
( 
status 
=> 
status  &
==' )
$str* 5
||6 8
status9 ?
==@ B
$strC I
)I J
. 
WithMessage 
( 
$str K
)K L
;L M
RuleFor 
( 
x 
=> 
x 
. 
ListingDate &
)& '
.' (
NotEmpty( 0
(0 1
)1 2
;2 3
RuleFor 
( 
x 
=> 
x 
. 
	ImageURLs $
)$ %
.% &
NotEmpty& .
(. /
)/ 0
;0 1
RuleFor 
( 
x 
=> 
x 
. 
UserID !
)! "
." #
NotEmpty# +
(+ ,
), -
;- .
} 	
private 
static 
bool 
BeAValidGuid (
(( )
Guid) -
guid. 2
)2 3
{ 	
return 
Guid 
. 
TryParse  
(  !
guid! %
.% &
ToString& .
(. /
)/ 0
,0 1
out2 5
_6 7
)7 8
;8 9
} 	
} 
} ⁄
{C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Commands\UpdatePropertyListingCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class (
UpdatePropertyListingCommand -
:. /
IRequest0 8
<8 9
Result9 ?
<? @
Guid@ D
>D E
>E F
{ 
public 
Guid 

PropertyId 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
string		 
?		 
Title		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
string

 
?

 
Address

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
string 
? 
Type 
{ 
get !
;! "
set# &
;& '
}( )
public 
double 
Price 
{ 
get !
;! "
set# &
;& '
}( )
public 
double 
SquareFootage #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
double 
NumberOfBedrooms &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBathrooms '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
? 
Status 
{ 
get  #
;# $
set% (
;( )
}* +
public 
DateTime 
ListingDate #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
? 
	ImageURLs  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
Guid 
UserID 
{ 
get  
;  !
set" %
;% &
}' (
} 
} ¢
{C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Commands\DeletePropertyListingCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class (
DeletePropertyListingCommand -
:. /
IRequest0 8
<8 9
Result9 ?
<? @
Guid@ D
>D E
>E F
{ 
public 
Guid 

PropertyId 
{  
get! $
;$ %
set& )
;) *
}+ ,
}		 
}

 ä
ÑC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Commands\CreatePropertyListingCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class 1
%CreatePropertyListingCommandValidator 6
:7 8
AbstractValidator9 J
<J K(
CreatePropertyListingCommandK g
>g h
{ 
public 1
%CreatePropertyListingCommandValidator 4
(4 5
)5 6
{ 	
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Address		 "
)		" #
.		# $
MaximumLength		$ 1
(		1 2
$num		2 5
)		5 6
;		6 7
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
Title

  
)

  !
.

! "
MaximumLength

" /
(

/ 0
$num

0 3
)

3 4
;

4 5
RuleFor 
( 
x 
=> 
x 
. 
Price  
)  !
.! "
GreaterThan" -
(- .
$num. /
)/ 0
;0 1
RuleFor 
( 
x 
=> 
x 
. 
SquareFootage (
)( )
.) *
GreaterThan* 5
(5 6
$num6 7
)7 8
;8 9
RuleFor 
( 
x 
=> 
x 
. 
NumberOfBedrooms +
)+ ,
., - 
GreaterThanOrEqualTo- A
(A B
$numB C
)C D
;D E
RuleFor 
( 
x 
=> 
x 
. 
NumberOfBathrooms ,
), -
.- . 
GreaterThanOrEqualTo. B
(B C
$numC D
)D E
;E F
RuleFor 
( 
x 
=> 
x 
. 
Description &
)& '
.' (
NotEmpty( 0
(0 1
)1 2
.2 3
MaximumLength3 @
(@ A
$numA D
)D E
;E F
RuleFor 
( 
x 
=> 
x 
. 
Status !
)! "
." #
NotEmpty# +
(+ ,
), -
." #
Must# '
(' (
status( .
=>/ 1
status2 8
==9 ;
$str< G
||H J
statusK Q
==R T
$strU [
)[ \
." #
WithMessage# .
(. /
$str/ ]
)] ^
;^ _
RuleFor 
( 
x 
=> 
x 
. 
ListingDate &
)& '
.' (
NotEmpty( 0
(0 1
)1 2
;2 3
RuleFor 
( 
x 
=> 
x 
. 
	ImageURLs $
)$ %
.% &
NotEmpty& .
(. /
)/ 0
;0 1
RuleFor 
( 
x 
=> 
x 
. 
UserID !
)! "
." #
NotEmpty# +
(+ ,
), -
;- .
} 	
} 
} æ
{C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\Commands\CreatePropertyListingCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Commands  (
{ 
public 

class (
CreatePropertyListingCommand -
:. /
IRequest0 8
<8 9
Result9 ?
<? @
Guid@ D
>D E
>E F
{ 
public 
string 
? 
Address 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
string		 
?		 
Title		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
string

 
?

 
Type

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
public 
double 
Price 
{ 
get !
;! "
set# &
;& '
}( )
public 
double 
SquareFootage #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
double 
NumberOfBedrooms &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBathrooms '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
? 
Status 
{ 
get  #
;# $
set% (
;( )
}* +
public 
DateTime 
ListingDate #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
? 
	ImageURLs  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
Guid 
UserID 
{ 
get  
;  !
set" %
;% &
}' (
} 
} Ü
âC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\CommandHandlers\UpdatePropertyListingCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{		 
public

 

class

 /
#UpdatePropertyListingCommandHandler

 4
:

5 6
IRequestHandler

7 F
<

F G(
UpdatePropertyListingCommand

G c
,

c d
Result

e k
<

k l
Guid

l p
>

p q
>

q r
{ 
private 
readonly &
IPropertyListingRepository 3

repository4 >
;> ?
private 
readonly 
IMapper  
mapper! '
;' (
public /
#UpdatePropertyListingCommandHandler 2
(2 3&
IPropertyListingRepository3 M

repositoryN X
,X Y
IMapperZ a
mapperb h
)h i
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /(
UpdatePropertyListingCommand/ K
requestL S
,S T
CancellationTokenU f
cancellationTokeng x
)x y
{ 	
var 
listing 
= 
mapper  
.  !
Map! $
<$ %
PropertyListing% 4
>4 5
(5 6
request6 =
)= >
;> ?
var 
result 
= 
await 

repository )
.) *
UpdateListingAsync* <
(< =
listing= D
)D E
;E F
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
}   ≈
âC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\CommandHandlers\DeletePropertyListingCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{ 
public 

class /
#DeletePropertyListingCommandHandler 4
:5 6
IRequestHandler7 F
<F G(
DeletePropertyListingCommandG c
,c d
Resulte k
<k l
Guidl p
>p q
>q r
{		 
private

 
readonly

 &
IPropertyListingRepository

 3

repository

4 >
;

> ?
public /
#DeletePropertyListingCommandHandler 2
(2 3&
IPropertyListingRepository3 M

repositoryN X
)X Y
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /(
DeletePropertyListingCommand/ K
requestL S
,S T
CancellationTokenU f
cancellationTokeng x
)x y
{ 	
var 
result 
= 
await 

repository )
.) *
DeleteListingAsync* <
(< =
request= D
.D E

PropertyIdE O
)O P
;P Q
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
} ì
âC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\PropertyListings\CommandHandlers\CreatePropertyListingCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
CommandHandlers  /
{		 
public

 

class

 /
#CreatePropertyListingCommandHandler

 4
:

5 6
IRequestHandler

7 F
<

F G(
CreatePropertyListingCommand

G c
,

c d
Result

e k
<

k l
Guid

l p
>

p q
>

q r
{ 
private 
readonly &
IPropertyListingRepository 3

repository4 >
;> ?
private 
readonly 
IMapper  
mapper! '
;' (
public /
#CreatePropertyListingCommandHandler 2
(2 3&
IPropertyListingRepository3 M

repositoryN X
,X Y
IMapperZ a
mapperb h
)h i
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /(
CreatePropertyListingCommand/ K
requestL S
,S T
CancellationTokenU f
cancellationTokeng x
)x y
{ 	
var 
propertyListing 
=  !
mapper" (
.( )
Map) ,
<, -
PropertyListing- <
>< =
(= >
request> E
)E F
;F G
var 
result 
= 
await 

repository )
.) *
AddListingAsync* 9
(9 :
propertyListing: I
)I J
;J K
if 
( 
result 
. 
	IsSuccess 
)  
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
}   
}!! π
ÅC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\QueryHandler\SearchAllPropertiesQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
QueryHandler0 <
{		 
public

 

class

 +
SearchAllPropertiesQueryHandler

 0
:

1 2
IRequestHandler

3 B
<

B C$
SearchAllPropertiesQuery

C [
,

[ \
Result

] c
<

c d
List

d h
<

h i
PropertyListingDto

i {
>

{ |
>

| }
>

} ~
{ 
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly $
IClientInquiryRepository 1

repository2 <
;< =
public +
SearchAllPropertiesQueryHandler .
(. /
IMapper/ 6
mapper7 =
,= >$
IClientInquiryRepository? W

repositoryX b
)b c
{ 	
this 
. 
mapper 
= 
mapper  
;  !
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &
PropertyListingDto& 8
>8 9
>9 :
>: ;
Handle< B
(B C$
SearchAllPropertiesQueryC [
request\ c
,c d
CancellationTokene v
cancellationToken	w à
)
à â
{ 	
var 
result 
= 
await 

repository )
.) *$
SearchAllPropertiesAsync* B
(B C
requestC J
.J K
SearchQueryK V
)V W
;W X
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
propertiesDto !
=" #
result$ *
.* +
Data+ /
./ 0
Select0 6
(6 7
property7 ?
=>@ B
mapperC I
.I J
MapJ M
<M N
PropertyListingDtoN `
>` a
(a b
propertyb j
)j k
)k l
.l m
ToListm s
(s t
)t u
;u v
return 
Result 
< 
List "
<" #
PropertyListingDto# 5
>5 6
>6 7
.7 8
Success8 ?
(? @
propertiesDto@ M
)M N
;N O
} 
else 
{ 
return 
Result 
< 
List "
<" #
PropertyListingDto# 5
>5 6
>6 7
.7 8
Failure8 ?
(? @
result@ F
.F G
ErrorMessageG S
)S T
;T U
}   
}!! 	
}"" 
}## Ê
|C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\QueryHandler\GetInquiryByIdQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
QueryHandler0 <
{		 
public

 

class

 &
GetInquiryByIdQueryHandler

 +
:

, -
IRequestHandler

. =
<

= >
GetInquiryByIdQuery

> Q
,

Q R
Result

S Y
<

Y Z
ClientInquiryDto

Z j
>

j k
>

k l
{ 
private 
readonly 
IMapper  
mapper! '
;' (
private 
readonly $
IClientInquiryRepository 1

repository2 <
;< =
public &
GetInquiryByIdQueryHandler )
() *
IMapper* 1
mapper2 8
,8 9$
IClientInquiryRepository: R

repositoryS ]
)] ^
{ 	
this 
. 
mapper 
= 
mapper  
;  !
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
ClientInquiryDto! 1
>1 2
>2 3
Handle4 :
(: ;
GetInquiryByIdQuery; N
requestO V
,V W
CancellationTokenX i
cancellationTokenj {
){ |
{ 	
var 
result 
= 
await 

repository )
.) *
GetInquiryByIdAsync* =
(= >
request> E
.E F
	InquiryIdF O
)O P
;P Q
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 

inquiryDto 
=  
mapper! '
.' (
Map( +
<+ ,
ClientInquiryDto, <
>< =
(= >
result> D
.D E
DataE I
)I J
;J K
return 
Result 
< 
ClientInquiryDto .
>. /
./ 0
Success0 7
(7 8

inquiryDto8 B
)B C
;C D
} 
else 
{ 
return 
Result 
< 
ClientInquiryDto .
>. /
./ 0
Failure0 7
(7 8
$"8 :
$str: Q
{Q R
requestR Y
.Y Z
	InquiryIdZ c
}c d
$strd n
"n o
)o p
;p q
}   
}!! 	
}"" 
}## Ô
ÇC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\QueryHandler\GetInquiryByClientIdQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
QueryHandler0 <
{		 
public

 

class

 ,
 GetInquiryByClientIdQueryHandler

 1
:

2 3
IRequestHandler

4 C
<

C D%
GetInquiryByClientIdQuery

D ]
,

] ^
Result

_ e
<

e f
List

f j
<

j k
ClientInquiryDto

k {
>

{ |
>

| }
>

} ~
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly $
IClientInquiryRepository 1
_repository2 =
;= >
public ,
 GetInquiryByClientIdQueryHandler /
(/ 0
IMapper0 7
mapper8 >
,> ?$
IClientInquiryRepository@ X

repositoryY c
)c d
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &
ClientInquiryDto& 6
>6 7
>7 8
>8 9
Handle: @
(@ A%
GetInquiryByClientIdQueryA Z
request[ b
,b c
CancellationTokend u
cancellationToken	v á
)
á à
{ 	
var 
result 
= 
await 
_repository *
.* +"
GetInquiriesByClientId+ A
(A B
requestB I
.I J
ClientIdJ R
)R S
;S T
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
inquiriesDto  
=! "
result# )
.) *
Data* .
.. /
Select/ 5
(5 6
inquiry6 =
=>> @
_mapperA H
.H I
MapI L
<L M
ClientInquiryDtoM ]
>] ^
(^ _
inquiry_ f
)f g
)g h
.h i
ToListi o
(o p
)p q
;q r
return 
Result 
< 
List "
<" #
ClientInquiryDto# 3
>3 4
>4 5
.5 6
Success6 =
(= >
inquiriesDto> J
)J K
;K L
} 
else 
{ 
return 
Result 
< 
List "
<" #
ClientInquiryDto# 3
>3 4
>4 5
.5 6
Failure6 =
(= >
result> D
.D E
ErrorMessageE Q
)Q R
;R S
}   
}!! 	
}"" 
}## ô
}C:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\QueryHandler\GetAllInquiriesQueryHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
QueryHandler0 <
{		 
public

 

class

 '
GetAllInquiriesQueryHandler

 ,
:

- .
IRequestHandler

/ >
<

> ? 
GetAllInquiriesQuery

? S
,

S T
Result

U [
<

[ \
List

\ `
<

` a
ClientInquiryDto

a q
>

q r
>

r s
>

s t
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly $
IClientInquiryRepository 1
_repository2 =
;= >
public '
GetAllInquiriesQueryHandler *
(* +
IMapper+ 2
mapper3 9
,9 :$
IClientInquiryRepository; S

repositoryT ^
)^ _
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &
ClientInquiryDto& 6
>6 7
>7 8
>8 9
Handle: @
(@ A 
GetAllInquiriesQueryA U
requestV ]
,] ^
CancellationToken_ p
cancellationToken	q Ç
)
Ç É
{ 	
var 
result 
= 
await 
_repository *
.* + 
GetAllInquiriesAsync+ ?
(? @
)@ A
;A B
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
var 
inquiriesDto  
=! "
result# )
.) *
Data* .
.. /
Select/ 5
(5 6
inquiry6 =
=>> @
_mapperA H
.H I
MapI L
<L M
ClientInquiryDtoM ]
>] ^
(^ _
inquiry_ f
)f g
)g h
.h i
ToListi o
(o p
)p q
;q r
return 
Result 
< 
List "
<" #
ClientInquiryDto# 3
>3 4
>4 5
.5 6
Success6 =
(= >
inquiriesDto> J
)J K
;K L
} 
else 
{ 
return 
Result 
< 
List "
<" #
ClientInquiryDto# 3
>3 4
>4 5
.5 6
Failure6 =
(= >
result> D
.D E
ErrorMessageE Q
)Q R
;R S
}   
}!! 	
}"" 
}## ö
uC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\Queries\SearchAllPropertiesQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
Queries0 7
{ 
public 

class $
SearchAllPropertiesQuery )
:* +
IRequest, 4
<4 5
Result5 ;
<; <
List< @
<@ A
PropertyListingDtoA S
>S T
>T U
>U V
{ 
public		 
required		 
string		 
SearchQuery		 *
{		+ ,
get		- 0
;		0 1
set		2 5
;		5 6
}		7 8
}

 
} ƒ
pC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\Queries\GetInquiryByIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
Queries0 7
{ 
public 

class 
GetInquiryByIdQuery $
:% &
IRequest' /
</ 0
Result0 6
<6 7
ClientInquiryDto7 G
>G H
>H I
{ 
public		 
Guid		 
	InquiryId		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
}

 
} ˇ
vC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\Queries\GetInquiryByClientIdQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
Queries0 7
{ 
public 

class %
GetInquiryByClientIdQuery *
:+ ,
IRequest- 5
<5 6
Result6 <
<< =
List= A
<A B
ClientInquiryDtoB R
>R S
>S T
>T U
{ 
public		 
Guid		 
ClientId		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
}

 
} €
qC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\Queries\GetAllInquiriesQuery.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
Queries0 7
{ 
public 

class  
GetAllInquiriesQuery %
:& '
IRequest( 0
<0 1
Result1 7
<7 8
List8 <
<< =
ClientInquiryDto= M
>M N
>N O
>O P
{ 
}

 
} È
xC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\Commands\UpdateClientInquiryCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
Commands0 8
{ 
public 

class &
UpdateClientInquiryCommand +
:, -
IRequest. 6
<6 7
Result7 =
<= >
Guid> B
>B C
>C D
{ 
public 
Guid 
	InquiryId 
{ 
get  #
;# $
set% (
;( )
}* +
public		 
List		 
<		 
string		 
>		 
?		 
Types		 "
{		# $
get		% (
;		( )
set		* -
;		- .
}		/ 0
public

 
double

 
MinPrice

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
double 
MaxPrice 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
double 
MinSquareFootage &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
MaxSquareFootage &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBedrooms &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBathrooms '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} »
xC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\Commands\DeleteClientInquiryCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
Commands0 8
{ 
public 

class &
DeleteClientInquiryCommand +
:, -
IRequest. 6
<6 7
Result7 =
<= >
Guid> B
>B C
>C D
{ 
public 
Guid 
	InquiryId 
{ 
get  #
;# $
set% (
;( )
}* +
}		 
}

 Ë
xC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\Commands\CreateClientInquiryCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
Commands0 8
{ 
public 

class &
CreateClientInquiryCommand +
:, -
IRequest. 6
<6 7
Result7 =
<= >
Guid> B
>B C
>C D
{ 
public 
Guid 
ClientId 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
List		 
<		 
string		 
>		 
?		 
Types		 "
{		# $
get		% (
;		( )
set		* -
;		- .
}		/ 0
public

 
double

 
MinPrice

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
double 
MaxPrice 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
double 
MinSquareFootage &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
MaxSquareFootage &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBedrooms &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBathrooms '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} ≠
ÜC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\CommandHandlers\UpdateClientInquiryCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
CommandHandlers0 ?
{		 
public

 

class

 -
!UpdateClientInquiryCommandHandler

 2
:

3 4
IRequestHandler

5 D
<

D E&
UpdateClientInquiryCommand

E _
,

_ `
Result

a g
<

g h
Guid

h l
>

l m
>

m n
{ 
private 
readonly $
IClientInquiryRepository 1

repository2 <
;< =
private 
readonly 
IMapper  
mapper! '
;' (
public -
!UpdateClientInquiryCommandHandler 0
(0 1$
IClientInquiryRepository1 I

repositoryJ T
,T U
IMapperV ]
mapper^ d
)d e
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /&
UpdateClientInquiryCommand/ I
requestJ Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
{ 	
var 
clientInquiry 
= 
mapper  &
.& '
Map' *
<* +
ClientInquiry+ 8
>8 9
(9 :
request: A
)A B
;B C
var 
result 
= 
await 

repository )
.) *
UpdateInquiryAsync* <
(< =
clientInquiry= J
)J K
;K L
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
} ·
ÜC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\CommandHandlers\DeleteClientInquiryCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
CommandHandlers0 ?
{ 
public 

class -
!DeleteClientInquiryCommandHandler 2
:3 4
IRequestHandler5 D
<D E&
DeleteClientInquiryCommandE _
,_ `
Resulta g
<g h
Guidh l
>l m
>m n
{		 
private

 
readonly

 $
IClientInquiryRepository

 1

repository

2 <
;

< =
public -
!DeleteClientInquiryCommandHandler 0
(0 1$
IClientInquiryRepository1 I

repositoryJ T
)T U
{ 	
this 
. 

repository 
= 

repository (
;( )
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /&
DeleteClientInquiryCommand/ I
requestJ Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
{ 	
var 
result 
= 
await 

repository )
.) *
DeleteInquiryAsync* <
(< =
request= D
.D E
	InquiryIdE N
)N O
;O P
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
} ™
ÜC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\ClientInquiries\CommandHandlers\CreateClientInquiryCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
ClientInquiries  /
./ 0
CommandHandlers0 ?
{		 
public

 

class

 -
!CreateClientInquiryCommandHandler

 2
:

3 4
IRequestHandler

5 D
<

D E&
CreateClientInquiryCommand

E _
,

_ `
Result

a g
<

g h
Guid

h l
>

l m
>

m n
{ 
private 
readonly $
IClientInquiryRepository 1

repository2 <
;< =
private 
readonly 
IMapper  
mapper! '
;' (
public -
!CreateClientInquiryCommandHandler 0
(0 1$
IClientInquiryRepository1 I

repositoryJ T
,T U
IMapperV ]
mapper^ d
)d e
{ 	
this 
. 

repository 
= 

repository (
;( )
this 
. 
mapper 
= 
mapper  
;  !
} 	
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /&
CreateClientInquiryCommand/ I
requestJ Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
{ 	
var 
clientInquiry 
= 
mapper  &
.& '
Map' *
<* +
ClientInquiry+ 8
>8 9
(9 :
request: A
)A B
;B C
var 
result 
= 
await 

repository )
.) *
AddInquiryAsync* 9
(9 :
clientInquiry: G
)G H
;H I
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
}   
}!! ∑
pC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Authentication\RegisterUserCommandValidator.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Authentication  .
.. /
Commands/ 7
{ 
public 

class (
RegisterUserCommandValidator -
:. /
AbstractValidator0 A
<A B
RegisterUserCommandB U
>U V
{ 
public (
RegisterUserCommandValidator +
(+ ,
), -
{. /
RuleFor 
( 
x 
=> 
x 
. 
Name 
)  
.  !
NotEmpty! )
() *
)* +
.+ ,
MaximumLength, 9
(9 :
$num: <
)< =
;= >
RuleFor		 
(		 
x		 
=>		 
x		 
.		 
Email		  
)		  !
.		! "
NotEmpty		" *
(		* +
)		+ ,
.		, -
EmailAddress		- 9
(		9 :
)		: ;
;		; <
RuleFor

 
(

 
x

 
=>

 
x

 
.

 
PhoneNumber

 &
)

& '
.

' (
NotEmpty

( 0
(

0 1
)

1 2
.

2 3
MaximumLength

3 @
(

@ A
$num

A C
)

C D
;

D E
RuleFor 
( 
x 
=> 
x 
. 
Password #
)# $
.$ %
NotEmpty% -
(- .
). /
./ 0
MaximumLength0 =
(= >
$num> @
)@ A
;A B
} 	
} 
} Ã
nC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Authentication\RegisterUserCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Authentication  .
.. /
Commands/ 7
{ 
public 

class &
RegisterUserCommandHandler +
:, -
IRequestHandler. =
<= >
RegisterUserCommand> Q
,Q R
ResultS Y
<Y Z
GuidZ ^
>^ _
>_ `
{ 
private		 
readonly		 
IUserAuthRepository		 ,

repository		- 7
;		7 8
public &
RegisterUserCommandHandler )
() *
IUserAuthRepository* =

repository> H
)H I
=>J L
thisM Q
.Q R

repositoryR \
=] ^

repository_ i
;i j
public 
async 
Task 
< 
Result  
<  !
Guid! %
>% &
>& '
Handle( .
(. /
RegisterUserCommand/ B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
{ 	
var 
user 
= 
new 
User 
{ 
Email 
= 
request 
.  
Email  %
,% &
Name 
= 
request 
. 
Name #
,# $
PhoneNumber 
= 
request %
.% &
PhoneNumber& 1
,1 2
PasswordHash 
= 
BCrypt %
.% &
Net& )
.) *
BCrypt* 0
.0 1
HashPassword1 =
(= >
request> E
.E F
PasswordF N
)N O
} 
; 
var 
result 
= 
await 

repository )
.) *
Register* 2
(2 3
user3 7
,7 8
cancellationToken9 J
)J K
;K L
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
Guid "
>" #
.# $
Success$ +
(+ ,
result, 2
.2 3
Data3 7
)7 8
;8 9
} 
return 
Result 
< 
Guid 
> 
.  
Failure  '
(' (
result( .
.. /
ErrorMessage/ ;
); <
;< =
} 	
} 
}   ÿ	
gC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Authentication\RegisterUserCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Authentication  .
.. /
Commands/ 7
{ 
public 

class 
RegisterUserCommand $
:% &
IRequest' /
</ 0
Result0 6
<6 7
Guid7 ;
>; <
>< =
{ 
public 
required 
string 
Email $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
required 
string 
Name #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public		 
required		 
string		 
PhoneNumber		 *
{		+ ,
get		- 0
;		0 1
set		2 5
;		5 6
}		7 8
public

 
required

 
string

 
Password

 '
{

( )
get

* -
;

- .
set

/ 2
;

2 3
}

4 5
} 
} æ
kC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Authentication\LoginUserCommandHandler.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Authentication  .
.. /
Commands/ 7
{ 
public 

class #
LoginUserCommandHandler (
:) *
IRequestHandler+ :
<: ;
LoginUserCommand; K
,K L
ResultM S
<S T
stringT Z
>Z [
>[ \
{ 
private		 
readonly		 
IUserAuthRepository		 ,
userRepository		- ;
;		; <
public #
LoginUserCommandHandler &
(& '
IUserAuthRepository' :
userRepository; I
)I J
{ 	
this 
. 
userRepository 
=  !
userRepository" 0
;0 1
} 	
public 
async 
Task 
< 
Result  
<  !
string! '
>' (
>( )
Handle* 0
(0 1
LoginUserCommand1 A
requestB I
,I J
CancellationTokenK \
cancellationToken] n
)n o
{ 	
var 
user 
= 
new 
User 
{ 
Email 
= 
request 
.  
Email  %
,% &
PasswordHash 
= 
request &
.& '
Password' /
} 
; 
var 
result 
= 
await 
userRepository -
.- .
Login. 3
(3 4
user4 8
)8 9
;9 :
if 
( 
result 
. 
	IsSuccess  
)  !
{ 
return 
Result 
< 
string $
>$ %
.% &
Success& -
(- .
result. 4
.4 5
Data5 9
)9 :
;: ;
} 
return 
Result 
< 
string  
>  !
.! "
Failure" )
() *
result* 0
.0 1
ErrorMessage1 =
)= >
;> ?
} 	
} 
} Ò
dC:\Users\cezar\desktop\RealEstateManagement\Application\Use Cases\Authentication\LoginUserCommand.cs
	namespace 	
Application
 
. 
	Use_Cases 
.  
Authentication  .
.. /
Commands/ 7
{ 
public 

class 
LoginUserCommand !
:" #
IRequest$ ,
<, -
Result- 3
<3 4
string4 :
>: ;
>; <
{ 
public		 
required		 
string		 
Email		 $
{		% &
get		' *
;		* +
set		, /
;		/ 0
}		1 2
public

 
required

 
string

 
Password

 '
{

( )
get

* -
;

- .
set

/ 2
;

2 3
}

4 5
} 
} °
GC:\Users\cezar\desktop\RealEstateManagement\Application\DTOs\UserDto.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 
UserDto 
{ 
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
? 
Name 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
? 
Email 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
? 
PhoneNumber "
{# $
get% (
;( )
set* -
;- .
}/ 0
}		 
}

 •
NC:\Users\cezar\desktop\RealEstateManagement\Application\DTOs\TransactionDto.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 
TransactionDto 
{ 
public 
Guid 
TransactionId !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
Guid 

PropertyId 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
Guid 
BuyerId 
{ 
get !
;! "
set# &
;& '
}( )
public 
Guid 
SellerId 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
double		 
	SalePrice		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
}

 
} ˆ
RC:\Users\cezar\desktop\RealEstateManagement\Application\DTOs\PropertyListingDto.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 
PropertyListingDto #
{ 
public 
Guid 

PropertyId 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
? 
Title 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
? 
Address 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
? 
Type 
{ 
get !
;! "
set# &
;& '
}( )
public		 
double		 
Price		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
double

 
SquareFootage

 #
{

$ %
get

& )
;

) *
set

+ .
;

. /
}

0 1
public 
double 
NumberOfBedrooms &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBathrooms '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
? 
Status 
{ 
get  #
;# $
set% (
;( )
}* +
public 
DateTime 
ListingDate #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
? 
	ImageURLs  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
Guid 
UserID 
{ 
get  
;  !
set" %
;% &
}' (
} 
} Ù
PC:\Users\cezar\desktop\RealEstateManagement\Application\DTOs\ClientInquiryDto.cs
	namespace 	
Application
 
. 
DTOs 
{ 
public 

class 
ClientInquiryDto !
{ 
public 
Guid 
	InquiryId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
Guid 
ClientId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
List 
< 
string 
> 
? 
Types "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
double 
MinPrice 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
double		 
MaxPrice		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
public

 
double

 
MinSquareFootage

 &
{

' (
get

) ,
;

, -
set

. 1
;

1 2
}

3 4
public 
double 
MaxSquareFootage &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBedrooms &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
double 
NumberOfBathrooms '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} ç
NC:\Users\cezar\desktop\RealEstateManagement\Application\DependencyInjection.cs
	namespace 	
Application
 
{		 
public

 

static

 
class

 
DependencyInjection

 +
{ 
public 
static 
IServiceCollection (
AddApplication) 7
(7 8
this8 <
IServiceCollection= O
servicesP X
)X Y
{ 	
services 
. 

AddMediatR 
(  
cfg  #
=># %
cfg% (
.( )(
RegisterServicesFromAssembly) E
(E F
AssemblyF N
.N O 
GetExecutingAssemblyO c
(c d
)d e
)e f
)f g
;g h
services 
. 
AddAutoMapper "
(" #
typeof# )
() *
MappingProfile* 8
)8 9
)9 :
;: ;
services 
. %
AddValidatorsFromAssembly .
(. /
Assembly/ 7
.7 8 
GetExecutingAssembly8 L
(L M
)M N
)N O
;O P
services 
. 
AddTransient !
(! "
typeof" (
(( )
IPipelineBehavior) :
<: ;
,; <
>< =
)= >
,> ?
typeof@ F
(F G
ValidationBehaviorG Y
<Y Z
,Z [
>[ \
)\ ]
)] ^
;^ _
services 
. 
	AddScoped 
< *
IPropertyListingFilterStrategy =
,= >
TypeFilterStrategy? Q
>Q R
(R S
)S T
;T U
services 
. 
	AddScoped 
< *
IPropertyListingFilterStrategy =
,= >
PriceFilterStrategy? R
>R S
(S T
)T U
;U V
services 
. 
	AddScoped 
< *
IPropertyListingFilterStrategy =
,= >'
SquareFootageFilterStrategy? Z
>Z [
([ \
)\ ]
;] ^
services 
. 
	AddScoped 
< *
IPropertyListingFilterStrategy =
,= >*
NumberOfBedroomsFilterStrategy? ]
>] ^
(^ _
)_ `
;` a
services 
. 
	AddScoped 
< *
IPropertyListingFilterStrategy =
,= >+
NumberOfBathroomsFilterStrategy? ^
>^ _
(_ `
)` a
;a b
services 
. 
	AddScoped 
< *
IPropertyListingFilterStrategy =
,= > 
StatusFilterStrategy? S
>S T
(T U
)U V
;V W
return 
services 
; 
} 	
} 
} á.
cC:\Users\cezar\desktop\RealEstateManagement\Application\AIML\PropertyListingPricePredictionModel.cs
	namespace 	
Application
 
. 
AIML 
{ 
public 

class /
#PropertyListingPricePredictionModel 4
{ 
private 
readonly 
	MLContext "
	mlContext# ,
;, -
private		 
ITransformer		 
model		 "
;		" #
public

 /
#PropertyListingPricePredictionModel

 2
(

2 3
)

3 4
=>

5 7
	mlContext

8 A
=

B C
new

D G
	MLContext

H Q
(

Q R
)

R S
;

S T
public 
void 
Train 
( 
List 
< 
PropertyListingData 2
>2 3

dataPoints4 >
)> ?
{ 	
var 
trainingData 
= 
	mlContext (
.( )
Data) -
.- .
LoadFromEnumerable. @
(@ A

dataPointsA K
)K L
;L M
var 
dataPrepPipeline  
=! "
	mlContext# ,
., -

Transforms- 7
.7 8
NormalizeMinMax8 G
(G H
nameofH N
(N O
PropertyListingDataO b
.b c
Featuresc k
)k l
)l m
;m n
var 
preprocessedData  
=! "
dataPrepPipeline# 3
.3 4
Fit4 7
(7 8
trainingData8 D
)D E
.E F
	TransformF O
(O P
trainingDataP \
)\ ]
;] ^
var 
options 
= 
new !
SdcaRegressionTrainer 3
.3 4
Options4 ;
{ 
LabelColumnName 
=  !
nameof" (
(( )
PropertyListingData) <
.< =
Label= B
)B C
,C D
FeatureColumnName !
=" #
nameof$ *
(* +
PropertyListingData+ >
.> ?
Features? G
)G H
,H I 
ConvergenceTolerance $
=% &
$num' ,
,, -%
MaximumNumberOfIterations )
=* +
$num, /
,/ 0
BiasLearningRate  
=! "
$num# '
} 
; 
var 
pipeline 
= 
dataPrepPipeline +
.+ ,
Append, 2
(2 3
	mlContext3 <
.< =

Regression= G
.G H
TrainersH P
.P Q
SdcaQ U
(U V
optionsV ]
)] ^
)^ _
;_ `
model   
=   
pipeline   
.   
Fit    
(    !
trainingData  ! -
)  - .
;  . /
}!! 	
public## 
float## 
Predict## 
(## 
PropertyListingData## 0
propertyListingData##1 D
)##D E
{$$ 	
var%% 
singleTestData%% 
=%%  
	mlContext%%! *
.%%* +
Data%%+ /
.%%/ 0
LoadFromEnumerable%%0 B
(%%B C
new%%C F
[%%F G
]%%G H
{%%I J
propertyListingData%%K ^
}%%_ `
)%%` a
;%%a b
var&& 
transformedTestData&& #
=&&$ %
model&&& +
.&&+ ,
	Transform&&, 5
(&&5 6
singleTestData&&6 D
)&&D E
;&&E F
var'' 

prediction'' 
='' 
	mlContext'' &
.''& '
Data''' +
.''+ ,
CreateEnumerable'', <
<''< =

Prediction''= G
>''G H
(''H I
transformedTestData''I \
,''\ ]
reuseRowObject''^ l
:''l m
false''n s
)''s t
.''t u
First''u z
(''z {
)''{ |
;''| }
return(( 

prediction(( 
.(( 
Score(( #
;((# $
})) 	
private++ 
class++ 

Prediction++  
{,, 	
public-- 
float-- 
Label-- 
{--  
get--! $
;--$ %
set--& )
;--) *
}--+ ,
public.. 
float.. 
Score.. 
{..  
get..! $
;..$ %
set..& )
;..) *
}..+ ,
}// 	
public11 
float11 
Evaluate11 
(11 
List11 "
<11" #
PropertyListingData11# 6
>116 7

dataPoints118 B
)11B C
{22 	
var33 
testData33 
=33 
	mlContext33 $
.33$ %
Data33% )
.33) *
LoadFromEnumerable33* <
(33< =

dataPoints33= G
)33G H
;33H I
var44 
predictions44 
=44 
model44 #
.44# $
	Transform44$ -
(44- .
testData44. 6
)446 7
;447 8
var55 
metrics55 
=55 
	mlContext55 #
.55# $

Regression55$ .
.55. /
Evaluate55/ 7
(557 8
predictions558 C
,55C D
labelColumnName55E T
:55T U
nameof55V \
(55\ ]
PropertyListingData55] p
.55p q
Label55q v
)55v w
)55w x
;55x y
return66 
(66 
float66 
)66 
metrics66 !
.66! "
RSquared66" *
;66* +
}77 	
}88 
}99 –
]C:\Users\cezar\desktop\RealEstateManagement\Application\AIML\PropertyListingDataPrediction.cs
	namespace 	
Application
 
. 
AIML 
{ 
public 

class )
PropertyListingDataPrediction .
{ 
public 
float 
Price 
{ 
get  
;  !
set" %
;% &
}' (
} 
} ∏"
]C:\Users\cezar\desktop\RealEstateManagement\Application\AIML\PropertyListingDataAggregator.cs
	namespace 	
Application
 
. 
AIML 
{ 
public 

class )
PropertyListingDataAggregator .
{ 
public 
List 
< 
PropertyListingData '
>' (
PropertyListingData) <
{= >
get? B
;B C
setD G
;G H
}I J
public )
PropertyListingDataAggregator ,
(, -
)- .
{ 	
PropertyListingData 
=  !
new" %
List& *
<* +
PropertyListingData+ >
>> ?
(? @
)@ A
;A B
}		 	
public 
List 
< 
PropertyListingData '
>' ("
GetPropertyListingData) ?
(? @
bool@ D
trainingDataE Q
=R S
trueT X
)X Y
{ 	
string 
filePath 
= 
$str (
;( )
int 
	lineCount 
= 
File  
.  !
	ReadLines! *
(* +
filePath+ 3
)3 4
.4 5
Count5 :
(: ;
); <
;< =
int 
currentLine 
= 
$num 
;  
foreach 
( 
var 
line 
in  
File! %
.% &
	ReadLines& /
(/ 0
filePath0 8
)8 9
)9 :
{ 
currentLine 
++ 
; 
if 
( 
trainingData  
==! #
true$ (
&&) +
currentLine, 7
>8 9
	lineCount: C
*D E
$numF J
)J K
break 
; 
if 
( 
trainingData  
==! #
false$ )
&&* ,
currentLine- 8
<=9 ;
	lineCount< E
*F G
$numH L
)L M
continue 
; 
var 
fields 
= 
line !
.! "
Split" '
(' (
$char( +
)+ ,
;, -
string 
Price 
= 
fields %
[% &
$num& '
]' (
;( )
string 
NumberOfBedrooms '
=( )
fields* 0
[0 1
$num1 2
]2 3
;3 4
string 
SquareFootage $
=% &
fields' -
[- .
$num. /
]/ 0
;0 1
if 
( 
Price 
== 
null !
||" $
NumberOfBedrooms% 5
==6 8
null9 =
||> @
SquareFootageA N
==O Q
nullR V
)V W
{ 
continue 
; 
} 
PropertyListingData   #
.  # $
Add  $ '
(  ' (
new  ( +
PropertyListingData  , ?
{!! 
Label"" 
="" 
float"" !
.""! "
Parse""" '
(""' (
Price""( -
)""- .
,"". /
Features## 
=## 
new## "
float### (
[##( )
]##) *
{##+ ,
float##- 2
.##2 3
Parse##3 8
(##8 9
NumberOfBedrooms##9 I
)##I J
,##J K
(##L M
float##M R
)##R S
(##S T
float##T Y
.##Y Z
Parse##Z _
(##_ `
SquareFootage##` m
)##m n
*##o p
$num##q y
)##y z
}##{ |
}$$ 
)$$ 
;$$ 
}%% 
return&& 
PropertyListingData&& &
;&&& '
}'' 	
public(( 
int(( '
GetPropertyListingDataCount(( .
(((. /
)((/ 0
{)) 	
return** 
PropertyListingData** &
.**& '
Count**' ,
;**, -
}++ 	
},, 
}-- €
SC:\Users\cezar\desktop\RealEstateManagement\Application\AIML\PropertyListingData.cs
	namespace 	
Application
 
. 
AIML 
{ 
public 

class 
PropertyListingData $
{ 
public 
float 
Label 
{ 
get  
;  !
set" %
;% &
}' (
[ 	

VectorType	 
( 
$num 
) 
] 
public		 
float		 
[		 
]		 
Features		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
}

 
} 