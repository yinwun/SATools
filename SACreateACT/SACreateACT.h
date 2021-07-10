#pragma once
#include<WinSock2.h>
#include<string.h>

#define MAX_ADAPTER_DESCRIPTION_LENGTH  128 // arb.
#define MAX_ADAPTER_NAME_LENGTH         256 // arb.
#define MAX_ADAPTER_ADDRESS_LENGTH      8   // arb.
#define DEFAULT_MINIMUM_ENTITIES        32  // arb.
#define MAX_HOSTNAME_LEN                128 // arb.
#define MAX_DOMAIN_NAME_LEN             128 // arb.
#define MAX_SCOPE_ID_LEN                256 // arb.
#define MAX_DHCPV6_DUID_LENGTH          130 // RFC 3315.
#define MAX_DNS_SUFFIX_STRING_LENGTH    256
#define SLICE_SIZE	65500
#define DEFAULTTABLE	\
	"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz{}"
#define _RUNNING_KEY "upupupupp"
#define FREE(x) HeapFree(GetProcessHeap(),0,(x))
#define MALLOC(x) HeapAlloc(GetProcessHeap(),0,(x))
#define SEPARATOR	";"
#define SLICE_MAX	20
#define DEFAULTFUNCEND		"#"
#define maxlen 1000

int SliceCount;
char MesgSlice[SLICE_MAX][SLICE_SIZE];
char charname[30];
char password[30];

typedef struct {
    char charname[30];
    char password[30];
}USERINFO;

typedef struct {
	char ip[30];
	int port;
}SERVERINFO;

/*
typedef struct _IP_ADDR_STRING {
    struct _IP_ADDR_STRING* Next;
    IP_ADDRESS_STRING IpAddress;
    IP_MASK_STRING IpMask;
    DWORD Context;
} IP_ADDR_STRING, * PIP_ADDR_STRING;

//
// ADAPTER_INFO - per-adapter information. All IP addresses are stored as
// strings
//

typedef struct _IP_ADAPTER_INFO {
    struct _IP_ADAPTER_INFO* Next;
    DWORD ComboIndex;
    char AdapterName[MAX_ADAPTER_NAME_LENGTH + 4];
    char Description[MAX_ADAPTER_DESCRIPTION_LENGTH + 4];
    UINT AddressLength;
    BYTE Address[MAX_ADAPTER_ADDRESS_LENGTH];
    DWORD Index;
    UINT Type;
    UINT DhcpEnabled;
    PIP_ADDR_STRING CurrentIpAddress;
    IP_ADDR_STRING IpAddressList;
    IP_ADDR_STRING GatewayList;
    IP_ADDR_STRING DhcpServer;
    BOOL HaveWins;
    IP_ADDR_STRING PrimaryWinsServer;
    IP_ADDR_STRING SecondaryWinsServer;
    time_t LeaseObtained;
    time_t LeaseExpires;
} IP_ADAPTER_INFO, * PIP_ADAPTER_INFO;
*/



int ConnectServer();
void CreateNewChar(int dataplace, char* charname, int imgno, int faceimgno, int vital, int str, int tgh, int dex, int earth, int water, int fire, int wind, int hometown);
int util_mkint(char* buffer, int value);
void util_swapint(int* dst, int* src, char* rule);
int util_256to64_shr(char* dst, char* src, int len, char* table, char* key);
int util_mkstring(char* buffer, char* value);
int util_256to64_shl(char* dst, char* src, int len, char* table, char* key);
void util_xorstring(char* dst, char* src);
void util_shlstring(char* dst, char* src, int offs);
int util_256to64(char* dst, char* src, int len, char* table);
void util_EncodeMessage(char* dst, char* src);
BOOL util_SendMesg(SOCKET* soc, int func, char* buffer);
void CheckUser(char* cdkey, char* pwd, char* serverkey);
void util_shlstring(char* dst, char* src, int offs);
//void GetNicInfoAddress(char* nicInfo);
BOOL util_Init_RuntimeKey(void);
void util_DecodeMessage(char* dst, char* src);
int util_64to256(char* dst, char* src, char* table);
void util_shrstring(char* dst, char* src, int offs);
BOOL util_SplitMessage(char* source, char* separator);
int util_GetFunctionFromSlice(int* func, int* fieldcount);
int util_deint(int sliceno, int* value);
int util_shl_64to256(char* dst, char* src, char* table, char* key);
int util_destring(int sliceno, char* value);
int util_shr_64to256(char* dst, char* src, char* table, char* key);
char* index(char* table, char src);
void clean_string(char* str);