--ALTER PLUGGABLE DATABASE OLS1 CLOSE INSTANCES=ALL;
--DROP PLUGGABLE DATABASE OLS1 INCLUDING DATAFILES;

alter user lbacsys identified by 123 account unlock container = all;
grant create procedure, update on nhom10.Staff, connect, resource to lbacsys;

alter session set "_oracle_script" = true;

create user nhom10 identified by 123;
grant connect, resource, unlimited tablespace to nhom10 with admin option;
grant create any table, alter any table to nhom10 with admin option;
grant create role, grant any role to nhom10 with admin option;
grant create user, drop user, alter user to nhom10 with admin option;
//========================================

grant nhom10.role to huunam66

create table ROOM(
    ID number GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
    Name        nvarchar2(50) not null, 
    Price       decimal(12, 2), 
    Status      nvarchar2(25), 
    Type_Room   varchar2(25),
    Max_People  number, 
    constraint PR_ROOM_ID primary key(ID),
    constraint UQ_ROOM_NAME unique(Name),
    constraint Check_ROOM__Max_People check (Max_People >= 1)
);
// Name         -> Ten phong
// Price        -> Gia thue
// Status       -> Trang thai phong
// Type_Room    -> loai phong ( VIP || THUONG)
// Max_People   -> So nguoi toi da

create table STAFF(
    ID number GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
    Name        nvarchar2(1024) not null,
    Email       varchar(1024),
    CMND        varchar2(1024) not null,
    Phone       varchar2(1024) not null,
    BirthDay    date,
    Gender      nvarchar2(1024) not null,
    Position    nvarchar2(1024),
    constraint PR_STAFF_ID primary key(ID),
    constraint UQ_STAFF_CMND unique(CMND),
    constraint UQ_STAFF_Phone unique(Phone),
    constraint UQ_STAFF_Email unique(Email)
);

// Name         -> ten nv
// CMND         -> chung minh nhan dan
// Phone        -> So dien thoai
// BirthDay     -> Ngay sinh
// Position     -> Chuc vu

create table ACCOUNT(
    STAFF_ID number,
    Username        varchar2(50) unique not null,
    Password        varchar2(1024) not null,
    Role            nvarchar2(50) not null,
    constraint PR_ACCOUNT_STAFF_ID primary key(STAFF_ID),
    constraint FK_ACCOUNT_STAFF_ID foreign key(STAFF_ID) references STAFF(ID) on delete cascade
);

create table KEY_SECURE(
    STAFF_ID number,
    Public_key      varchar2(2000),
    Private_key     varchar2(2000),
    Symmetric_key   varchar2(2000),
    constraint PR__KEY_SECURE__ACCOUNT_ID primary key(STAFF_ID),
    constraint FK__KEY_SECURE__ACCOUNT_ID foreign key(STAFF_ID) references STAFF(ID) on delete cascade
);


create table CLIENT(
    ID number GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
    Name        nvarchar2(50) not null,
    CMND        varchar2(2048) not null,
    Phone       varchar2(11) not null,
    Gender      nvarchar2(4) not null,
    constraint PR_CLIENT_ID primary key(ID),
    constraint UQ_CLIENT_CMND unique(CMND)
);
// Name     -> ten nv
// Phone    -> chung minh nhan dan
// SDT      -> So dien thoai

// *******

create table BOOKING(
    ID number GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
    Phone_Client    varchar(2048) not null,
    Name_Client     nvarchar2(50) not null,
    ID_STAFF        number not null,
    ID_ROOM         number not null,
    Preset_Money    decimal(12, 2),
    Day_Booking     date,
    Day_Check_In    date,
    Day_Check_Out   date,
    constraint PR_BOOKING_ID primary key(ID),
    constraint FK_BOOKING__ID_ROOM foreign key(ID_ROOM) references ROOM(ID) on delete set null,
    constraint FK_BOOKING__ID_STAFF foreign key(ID_STAFF) references STAFF(ID) on delete set null
);
// Phone_Client     -> so dien thoai nguoi book
// Day_Book         -> ngay book
// Day_Check_In     -> ngay nhan phong
// Preset_Money     -> tien dat truoc (20% gia phong)
// Day_Check_Out    -> ngay tra phong vi co them gio, du lieu thuoc tinh nay se duoc luu duoi dang dd-MM-yyyy H:m:s
create table BILL(
    ID number GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
    ID_CLIENT       number,
    ID_STAFF        number,
    Day_for_pay     date,
    Count_Room      number,
    Status          varchar(50),
    Total           decimal(15, 2),
    constraint PR_BILL_ID primary key(ID),
    constraint FK_BILL__ID_CLIENT foreign key(ID_CLIENT) references CLIENT(ID) on delete set null,
    constraint FK_BILL__ID_STAFF foreign key(ID_STAFF) references STAFF(ID) on delete set null,
    constraint Check_BILL_Count_Room check (Count_Room >= 1)
);
// Day_for_pay -> ngay thanh toan
// Count_Room -> so luong phong da thue
// Total -> tong tien phong
// Status -> trang thai hoa don 

create table BILL_DETAILS(
    ID_BILL         number,
    ID_ROOM         number,
    Price_Room      decimal(12, 2),
    Preset_Money    decimal(12, 2),
    Discount        float,
    Day_Check_In    date,
    Day_Check_Out   date,
    constraint PR_BILL_DETAILS primary key(ID_BILL, ID_ROOM),
    constraint FK_BILL_DETAILS__ID_BILL foreign key(ID_BILL) references BILL(ID) on delete set null,
    constraint FK_BILL_DETAILS__ID_ROOM foreign key(ID_ROOM) references ROOM(ID) on delete set null,
    constraint Check_BILL_DETAILS__Discount check (Discount >= 0 and Discount <= 100)
);

CREATE ROLE BASIC_NHANVIEN;
GRANT SELECT, INSERT, UPDATE ON nhom10.BOOKING TO BASIC_NHANVIEN;
GRANT SELECT ON nhom10.ROOM TO BASIC_NHANVIEN;
GRANT SELECT ON nhom10.STAFF TO BASIC_NHANVIEN;

CREATE ROLE BASIC_KETOAN;
GRANT SELECT, INSERT ON nhom10.BILL TO BASIC_KETOAN;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.BILL_DETAILS TO BASIC_KETOAN;
GRANT SELECT, INSERT ON nhom10.BOOKING TO BASIC_KETOAN;
GRANT SELECT ON nhom10.ROOM TO BASIC_KETOAN;
GRANT SELECT ON nhom10.STAFF TO BASIC_KETOAN;

CREATE ROLE BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.ACCOUNT TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE ON nhom10.BILL TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE ON nhom10.BILL_DETAILS TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE ON nhom10.BOOKING TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.CLIENT TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.ROOM TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.STAFF TO BASIC_QUANLY;

GRANT CONNECT TO BASIC_NHANVIEN,BASIC_KETOAN, BASIC_QUANLY;

GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.STAFF TO LBACSYS;

delete from nhom10.STAFF
select * from nhom10.STAFF
--SET SERVEROUTPUT ON;
--DECLARE
--    RESPONE VARCHAR2(50);
--BEGIN
--    nhom10.REGISTER_ACCOUNT('huunam126346', '123123', 'Qu?n lý', RESPONE);
--
--    DBMS_OUTPUT.PUT_LINE(RESPONE);  
--END;

// THU TUC TAO TAI KHOAN MOI CHO NHAN VIEN
CREATE OR REPLACE PROCEDURE GRANT_PRIVILEGES_ROLES(
    USERNAME IN VARCHAR2,
    NEW_POSITION IN NVARCHAR2,
    RESPONE OUT VARCHAR2
)
IS
BEGIN
    IF UPPER(NEW_POSITION) = 'NHÂN VIÊN' 
    THEN 
        EXECUTE IMMEDIATE 'GRANT BASIC_NHANVIEN TO ' || USERNAME;
    END IF;

    IF UPPER(NEW_POSITION) = 'K? TOÁN' 
    THEN
        EXECUTE IMMEDIATE 'GRANT BASIC_KETOAN TO ' || USERNAME;
    END IF;

    IF UPPER(NEW_POSITION) = 'QU?N LÝ' 
    THEN
        EXECUTE IMMEDIATE 'GRANT BASIC_QUANLY TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.DECRYPT_DOIXUNG TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.ENCRYPT_DOIXUNG TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.GRANT_PRIVILEGES_ROLES TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.REVOKE_PRIVILEGES_ROLES TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON SYS.DBMS_CRYPTO TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON SYS.DBMS_CRYPTO_FFI TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON DBMS_CRYPTO TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT CONNECT, ALTER ANY TABLE TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT CREATE ROLE, GRANT ANY ROLE TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT CREATE USER, DROP USER, ALTER USER TO ' || USERNAME;
    END IF;
    
    RESPONE := 'TRUE';
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := 'FALSE';
END;


CREATE OR REPLACE PROCEDURE REVOKE_PRIVILEGES_ROLES(
    USERNAME IN VARCHAR2,
    OLD_POSITION IN NVARCHAR2,
    RESPONE OUT VARCHAR2
)
IS
BEGIN
    IF UPPER(OLD_POSITION) = 'NHÂN VIÊN' 
    THEN 
        EXECUTE IMMEDIATE 'REVOKE ROLE BASIC_NHANVIEN FROM ' || USERNAME;
    END IF;

    IF UPPER(OLD_POSITION) = 'K? TOÁN' 
    THEN
        EXECUTE IMMEDIATE 'REVOKE ROLE BASIC_KETOAN FROM ' || USERNAME;
    END IF;

    IF UPPER(OLD_POSITION) = 'QU?N LÝ' 
    THEN
        EXECUTE IMMEDIATE 'REVOKE ROLE BASIC_QUANLY FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.DECRYPT_DOIXUNG FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.ENCRYPT_DOIXUNG FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.GRANT_PRIVILEGES_ROLES FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.REVOKE_PRIVILEGES_ROLES FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON SYS.DBMS_CRYPTO FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON SYS.DBMS_CRYPTO_FFI FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON DBMS_CRYPTO FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE ALTER ANY TABLE FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE CREATE ROLE, GRANT ANY ROLE FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE CREATE USER, DROP USER, ALTER USER FROM ' || USERNAME;
    END IF;
     
    RESPONE := 'TRUE';
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := 'FALSE';
END;



grant execute on SYS.DBMS_CRYPTO to nhom10; 
grant execute on SYS.DBMS_CRYPTO_FFI to nhom10;
grant execute on DBMS_CRYPTO to nhom10;


// ------------------------- MA HOA -------------------------

CREATE OR REPLACE PROCEDURE ENCRYPT_DOIXUNG(
    p_input IN VARCHAR2,
    p_key IN VARCHAR2,
    Result_ENCRYPT OUT VARCHAR2
) 
IS
    l_input_raw RAW(2048) := UTL_RAW.CAST_TO_RAW(p_input);
    l_key_raw RAW(2048) := UTL_RAW.CAST_TO_RAW(p_key);
    l_raw RAW(2048);
BEGIN
    l_raw := DBMS_CRYPTO.ENCRYPT(
        SRC => l_input_raw,
        TYP => DBMS_CRYPTO.des_cbc_pkcs5,
        KEY => l_key_raw
    );
    
    Result_ENCRYPT := TO_CHAR(l_raw);
    
    EXCEPTION WHEN OTHERS 
    THEN 
        Result_ENCRYPT := NULL;
END;

// ------------------------- GIAI MA -------------------------

CREATE OR REPLACE PROCEDURE DECRYPT_DOIXUNG
(
    p_input IN VARCHAR2, 
    p_key IN VARCHAR2,
    Result_DECRYPT OUT VARCHAR2
)
IS
    l_key_raw RAW(2048) := UTL_RAW.CAST_TO_RAW(p_key);
    l_raw RAW(2048);
BEGIN

    SELECT HEXTORAW(p_input) into l_raw from dual;
    DBMS_OUTPUT.PUT_LINE(l_raw);
    l_raw := DBMS_CRYPTO.DECRYPT(
        SRC => l_raw,
        TYP => DBMS_CRYPTO.des_cbc_pkcs5,
        KEY => l_key_raw
    );   
--    DBMS_OUTPUT.PUT_LINE(l_raw);
    Result_DECRYPT := UTL_RAW.CAST_TO_VARCHAR2(l_raw);
--    DBMS_OUTPUT.PUT_LINE(Result_DECRYPT);

    EXCEPTION WHEN OTHERS 
    THEN 
        Result_DECRYPT := NULL;          
END;

grant execute on nhom10.CREATE_GRANT_USER to huunam66

---------------------------- TEST ----------------------------
// ------------------------- MA HOA -------------------------------

SET SERVEROUTPUT ON;
DECLARE
    Result_ENCRYPT_to varchar2(2000);
BEGIN 
    ENCRYPT_DOIXUNG('<RSAKeyValue><Modulus>yFugw2tEfzizubgUr/CvVhOGb8UodknGU4mvAm8II9+JccVF3cCBdQbOefoTbx5wTn2P3JpU3nY5UlS0v3ut6DgHqSfUKQsXjJg0eA2oWMgj2ITB/FSIrdGLSMWPj3/XOWC0UMC3/ComPgZDZ5028irMvubP3MyJaQpjMqufWn0=</Modulus><Exponent>AQAB</Exponent><P>y3K+Ja2UU4P2NSvc3/dx1KHTUHWreKU4CigzVEYiJgMpEKo1iWtkAFEZ5EJAU46zu9fIkhFIvQfqDazS/QK6Xw==</P><Q>/ByJM8AYabmtuU7XPFTsQcYXiE/XVD/c+rCiEzENJySUTE4A37rdnpMXDIYjihZpAcn58q5pATyG3ExN83tQow==</Q><DP>wecEb2i2m7qD6D178EIoTZ5GhsL/wKbHeHbEJSgK1+vfMbDoAadG0j55zOGP6ZNyni+VTBIJH5DxdEMpfFcjRQ==</DP><DQ>s+/OARCOnA75LfRWbGoUQZGXxCNwBWKbXpVo6BevUouqCJf3ybb+bCqBXd/zR6BdC/jTG2Fd8pL3kg4n6KrPzw==</DQ><InverseQ>TU13ozYSwEN0hOqidxpnZjPEZN2cwl7oJsXx8PBmin0amA5b1D2VacBUxzRTsQ6Ed+x6jmvpNxVGzBByBCbBiQ==</InverseQ><D>iWkmGcBwqtwljwLr1Tq51cvwUKmkNXQle9ea2o1xxMCjkA6e+xXy82LmqwtS1svGdF5zEvYZrLTiXU/Q4t6dBsUuGs4mxavS/lmhGPxoe8f3vmpM7sCS99GztaHcD+XTKreZA2v5eznU5ApOYuS4zOphJ9TNh+nybUY12NFBaDU=</D></RSAKeyValue>', 'b6aef28ccf6b85f3879d39623da0423e', Result_ENCRYPT_to);
    DBMS_OUTPUT.PUT_LINE('RESULT ENCRYPT: ' || Result_ENCRYPT_to);
END;

---------------------------- TEST -------------------------;
// ------------------------- GIAI MA -------------------------------
    
set serveroutput on;
declare
    result_1 varchar(2000);
begin
    DECRYPT_DOIXUNG('F92940D2764FA2B9F8E3263976F508616495E061A60BD59A5B6215F909D0B7E34E4874EE4BFBF6A3C0BA54C7FEFE072D4A69D3376A681D1752C4A2B4CF93B90EF38022251C748C8E8D0078327445F42CDB7F706A72462FAC2366780D71104F5129A986084564B819610625CA64A234E7E43C2EA4AEE9D6EE4BEF54F390CD39198215D1064376F5B7093AC3EA9A75726D89EA451214F9F174DA22E2E5ADF4A738D6EB1C9A04D00521EDCFA5B550E6FCFAC3F4A35679101D5302B63F0704413F72416BEB19FC1B2E7D9F544B2A9E59F5BAA5CBEFBC351F556433C5C0A7D439FD3CCB274DB617168B134F0A8419479D1113B9919A7A6A934C72BB9EB57B668C4C938A0E6C80E9B1D12AADE7A55F2483F877285315FD99B8101332A3DD8B042F0AABB97DAFD838B1DBB42FD31BAA2E6FA12EDAF88C0B5E09F45CC0C8B3E68C6BB0BEFF2870E392373B56E81968D449168E24A81DD6720146546728B886EEA46FD08DFCFC32BD730EC3E624208BD662A6842C8EC3D8BB7069E315DBC170061F018D4A5C30417DD32C80B177CB0B289394269647237B2FD1E9D56906ED5158FF4884D118057959AF9BF95B631B4671B4C0E75AE79D2285B2EEB6E76B7A7CF474E0341F5480A7063453A3A26569A194B6A6B026AEBFCBFC72FE148083CAA71B24BEB85265BD13AF783EEE89E7E3ADABCE2DBDE88EFE0E6DA1161ED50407B71D257EB85B13AA13C1DDDFC55E7D1A7C56F9C9ECB506E4067A8F87AEF767191AC7ADCE0647C45D936E5FEF350CDBBE838BDE8DE44F975BBCB9609E060F4E891E2E864BB9D3B4DBDEB16DA14557C7DA05D9B98FC9EB889731ED93F941CD429AA2DC49CC3EA869C99743308C0EFD2112E2BD8D476C134D2F338F9D4CC201302842C6C376048316582DB2CA20DA1BCCC2F69F89910F71AA23E2E8425E54F0C968B7033EE716D33235BEF06F23248C4CD1193F875BFA4FF0722E273BD7051761B3C2815B782F7B7AC85A1C6B49C485E4A69DCCF9C3C59A663D62ECBBCE2F11239AF41163A2F10DACDB884F80E2B8EBAC204BF0DA1A25EBEF5198D46D682F8280A17B91FA6406E7745A1BCFB286235380CB2B26DF9F4D3F9C305F50D7E333FCDFFDED5E7B9DAEB0F38FE8C9C0434657E2CA1F3F94C42C041EFB86B15FE9212374629550430AA2389C665CF1D1BF3C37BE52E68BA822F3A7AA2EAE5F643647A48F62B8207D6A4CE47FF744E697E333FB3EFD63313425C2709B303089FA9B7BBE7767BC4BDBEA54B8751F535B9325EEFFB66E8273537A6866B70634C1F9CD9AE8', 'b6aef28ccf6b85f3879d39623da0423e', result_1);
    DBMS_OUTPUT.PUT_LINE(result_1);
end;


// PROCEDURE SET LEVEL OLS FOR USER OF LBACSYS

CREATE OR REPLACE PROCEDURE SET_LEVEL_OLS_SCHEMA_NHOM10_FOR_USER(
    USERNAME    IN VARCHAR2,
    POSITION    IN NVARCHAR2,
    RESPONE     OUT VARCHAR2
)
IS
BEGIN
    IF UPPER(POSITION) = 'NHÂN VIÊN'
    THEN
        EXECUTE IMMEDIATE 'BEGIN SA_USER_ADMIN.SET_LEVELS(policy_name => ''ChinhSach_OLS_STAFF_Schema_nhom10'', user_name => '''|| 
                USERNAME || ''', max_level => ''NV'', min_level => ''NV'', def_level => ''NV'', row_level => ''NV''); END;';
    END IF;
    
    IF UPPER(POSITION) = 'K? TOÁN'
    THEN
        EXECUTE IMMEDIATE 'BEGIN SA_USER_ADMIN.SET_LEVELS(policy_name => ''ChinhSach_OLS_STAFF_Schema_nhom10'', user_name => '''|| 
                USERNAME || ''', max_level => ''KT'', min_level => ''NV'', def_level => ''KT'', row_level => ''KT''); END;';
    END IF;
    
    IF UPPER(POSITION) = 'QU?N LÝ'
    THEN
        EXECUTE IMMEDIATE 'BEGIN SA_USER_ADMIN.SET_LEVELS(policy_name => ''ChinhSach_OLS_STAFF_Schema_nhom10'', user_name => '''|| 
                USERNAME || ''', max_level => ''QL'', min_level => ''NV'', def_level => ''QL'', row_level => ''QL''); END;';
    END IF;
    
    RESPONE := 'TRUE';
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := 'FALSE';
END;

select * from nhom10.STAFF


grant alter on nhom10.staff to lbacsys

alter table nhom10.staff drop column label


GRANT EXECUTE ON nhom10.DECRYPT_DOIXUNG TO huunam123;
GRANT EXECUTE ON nhom10.ENCRYPT_DOIXUNG TO huunam123;
GRANT EXECUTE ON nhom10.GRANT_PRIVILEGES_ROLES TO huunam123;
GRANT EXECUTE ON nhom10.REVOKE_PRIVILEGES_ROLES TO huunam123
GRANT EXECUTE ON SYS.DBMS_CRYPTO TO huunam123
GRANT EXECUTE ON SYS.DBMS_CRYPTO_FFI TO huunam123
GRANT EXECUTE ON DBMS_CRYPTO TO huunam123
GRANT CONNECT, ALTER ANY TABLE TO huunam123
GRANT CREATE ROLE, GRANT ANY ROLE TO huunam123
GRANT CREATE USER, DROP USER, ALTER USER TO huunam123


REVOKE EXECUTE ON nhom10.DECRYPT_DOIXUNG FROM huunam123
REVOKE EXECUTE ON nhom10.ENCRYPT_DOIXUNG FROM huunam123
REVOKE EXECUTE ON nhom10.GRANT_PRIVILEGES_ROLES FROM huunam123
REVOKE EXECUTE ON nhom10.REVOKE_PRIVILEGES_ROLES FROM huunam123
REVOKE EXECUTE ON SYS.DBMS_CRYPTO FROM huunam123
REVOKE EXECUTE ON SYS.DBMS_CRYPTO_FFI FROM huunam123
REVOKE EXECUTE ON DBMS_CRYPTO FROM huunam123
REVOKE ALTER ANY TABLE FROM huunam123
REVOKE CREATE ROLE, GRANT ANY ROLE FROM huunam123
REVOKE CREATE USER, DROP USER, ALTER USER FROM huunam123

