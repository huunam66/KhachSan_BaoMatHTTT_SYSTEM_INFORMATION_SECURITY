--ALTER PLUGGABLE DATABASE OLS1 CLOSE INSTANCES=ALL;
--DROP PLUGGABLE DATABASE OLS1 INCLUDING DATAFILES;
alter session set "_oracle_script" = true;

create user nhom10 identified by 123;
grant connect, resource, unlimited tablespace to nhom10 with admin option;
grant create any table, alter any table to nhom10 with admin option;
grant create role, grant any role to nhom10 with admin option;
grant create user, drop user, alter user to nhom10 with admin option;
//========================================

create table ROOM(
    ID number GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
    Name        nvarchar2(50) not null, 
    Price       number, 
    Preset_Money    number,
    Status      nvarchar2(25), 
    Type_Room   nvarchar2(25),
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
    Name        nvarchar2(50) not null,
    Email       varchar(2000),
    CMND        varchar2(2000) not null,
    Phone       varchar2(2000) not null,
    BirthDay    date,
    Gender      nvarchar2(5) not null,
    Position    nvarchar2(20),
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
    Password        varchar2(2000) not null,
    Role            nvarchar2(50) not null,
    constraint PR_ACCOUNT_STAFF_ID primary key(STAFF_ID),
    constraint FK_ACCOUNT_STAFF_ID foreign key(STAFF_ID) references STAFF(ID) on delete cascade
);

select * from nhom10.STAFF

create table CUSTOMER(
    ID number GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1) NOT NULL,
    Name        nvarchar2(50) not null,
    CMND        varchar2(2000) not null,
    Phone       varchar2(2000) not null,
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
    Preset_Money    number,
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
    Total           number,
    constraint PR_BILL_ID primary key(ID),
    constraint FK_BILL__ID_CLIENT foreign key(ID_CLIENT) references CUSTOMER(ID) on delete set null,
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
    Price_Room      number,
    Preset_Money    number,
    Discount        float,
    Day_Check_In    date,
    Day_Check_Out   date,
    constraint PR_BILL_DETAILS primary key(ID_BILL, ID_ROOM),
    constraint FK_BILL_DETAILS__ID_BILL foreign key(ID_BILL) references BILL(ID) on delete set null,
    constraint FK_BILL_DETAILS__ID_ROOM foreign key(ID_ROOM) references ROOM(ID) on delete set null,
    constraint Check_BILL_DETAILS__Discount check (Discount >= 0 and Discount <= 100)
);


=============================== PROCEDURE CRUD BASIC DB ===================================================

CREATE OR REPLACE PROCEDURE INSERT_STAFF(
    inp_NAME IN NVARCHAR2,
    inp_EMAIL IN VARCHAR2,
    inp_CMND IN VARCHAR2,
    inp_PHONE IN VARCHAR2,
    inp_BIRTHDAY IN DATE,
    inp_GENDER IN NVARCHAR2,
    inp_POSITION IN NVARCHAR2,
    inp_LABEL IN VARCHAR2
)
IS
BEGIN
    INSERT INTO nhom10.STAFF
    (NAME, EMAIL, CMND, PHONE, BIRTHDAY, GENDER, POSITION, LABEL)
    VALUES
    (inp_NAME, inp_EMAIL, inp_CMND, inp_PHONE, inp_BIRTHDAY, inp_GENDER, inp_POSITION, TO_NUMBER(inp_LABEL));
END;


CREATE OR REPLACE PROCEDURE UPDATE_STAFF(
    inp_ID IN VARCHAR2,
    inp_NAME IN NVARCHAR2,
    inp_EMAIL IN VARCHAR2,
    inp_CMND IN VARCHAR2,
    inp_PHONE IN VARCHAR2,
    inp_BIRTHDAY IN DATE,
    inp_GENDER IN NVARCHAR2,
    inp_POSITION IN NVARCHAR2
)
IS
BEGIN
    UPDATE nhom10.STAFF
    SET
    NAME = inp_NAME, 
    EMAIL = inp_EMAIL, 
    CMND = inp_CMND, 
    PHONE = inp_PHONE, 
    BIRTHDAY = inp_BIRTHDAY, 
    GENDER = inp_GENDER, 
    POSITION = inp_POSITION
    WHERE ID = TO_NUMBER(inp_ID);
END;

CREATE OR REPLACE PROCEDURE INSERT_ACCOUNT(
    inp_STAFF_ID IN VARCHAR2,
    inp_USERNAME IN VARCHAR2,
    inp_PASSWORD IN VARCHAR2,
    inp_ROLE IN NVARCHAR2,
    RESPONE OUT BOOLEAN
)
IS
BEGIN
    INSERT INTO nhom10.ACCOUNT(STAFF_ID, USERNAME, PASSWORD, ROLE)
    VALUES
    (TO_NUMBER(inp_STAFF_ID), inp_USERNAME, inp_PASSWORD, inp_ROLE);
    
    RESPONE := TRUE;
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := FALSE;
END;

CREATE OR REPLACE PROCEDURE UPDATE_ROLE(
    inp_STAFF_ID IN VARCHAR2,
    inp_ROLE IN NVARCHAR2,
    RESPONE OUT BOOLEAN
)
IS
BEGIN
    UPDATE nhom10.ACCOUNT
    SET
    ROLE = inp_ROLE
    WHERE STAFF_ID = TO_NUMBER(inp_STAFF_ID);
    
    RESPONE := TRUE;
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := FALSE;
END;


CREATE OR REPLACE PROCEDURE INSERT_CUSTOMER(
    inp_NAME IN NVARCHAR2,
    inp_CMND IN VARCHAR2,
    inp_PHONE IN VARCHAR2,
    inp_GENDER IN NVARCHAR2,
    RESPONE OUT BOOLEAN
)
IS
BEGIN
    INSERT INTO nhom10.CUSTOMER
    (NAME, CMND, PHONE, GENDER)
    VALUES
    (inp_NAME, inp_CMND, inp_PHONE, inp_GENDER);
    
    RESPONE := TRUE;
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := FALSE;
END;


CREATE OR REPLACE PROCEDURE INSERT_CUSTOMER(
    inp_NAME IN NVARCHAR2,
    inp_CMND IN VARCHAR2,
    inp_PHONE IN VARCHAR2,
    inp_GENDER IN NVARCHAR2,
    RESPONE OUT BOOLEAN
)
IS
BEGIN
    INSERT INTO nhom10.CUSTOMER
    (NAME, CMND, PHONE, GENDER)
    VALUES
    (inp_NAME, inp_CMND, inp_PHONE, inp_GENDER);
    
    RESPONE := TRUE;
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := FALSE;
END;


CREATE OR REPLACE PROCEDURE UPDATE_CUSTOMER(
    inp_ID IN VARCHAR2,
    inp_NAME IN NVARCHAR2,
    inp_CMND IN VARCHAR2,
    inp_PHONE IN VARCHAR2,
    inp_GENDER IN NVARCHAR2,
    RESPONE OUT BOOLEAN
)
IS
BEGIN
    UPDATE nhom10.CUSTOMER
    SET
    NAME = inp_NAME,
    CMND = inp_CMND,
    PHONE = inp_PHONE,
    GENDER = inp_GENDER
    WHERE ID = TO_NUMBER(inp_ID);
    
    RESPONE := TRUE;
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := FALSE;
END;

CREATE OR REPLACE PROCEDURE INSERT_ROOM(
    inp_NAME IN NVARCHAR2,
    inp_PRICE IN VARCHAR2,
    inp_PRESET_MONEY IN VARCHAR2,
    inp_STATUS IN NVARCHAR2,
    inp_TYPE_ROOM IN NVARCHAR2,
    inp_MAX_PEOPLE IN VARCHAR2,
    RESPONE OUT BOOLEAN
)
IS
BEGIN
    INSERT INTO nhom10.ROOM
    (NAME, PRICE, PRESET_MONEY, STATUS, TYPE_ROOM, MAX_PEOPLE)
    VALUES
    (
        inp_NAME, 
        TO_NUMBER(inp_PRICE),
        TO_NUMBER(inp_PRESET_MONEY),
        inp_STATUS, 
        inp_TYPE_ROOM,
        TO_NUMBER(inp_MAX_PEOPLE)
    );
        
    RESPONE := TRUE;
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := FALSE;
END;


CREATE OR REPLACE PROCEDURE UPDATE_ROOM(
    inp_ID IN VARCHAR2,
    inp_NAME IN NVARCHAR2,
    inp_PRICE IN VARCHAR2,
    inp_PRESET_MONEY IN VARCHAR2,
    inp_STATUS IN NVARCHAR2,
    inp_TYPE_ROOM IN NVARCHAR2,
    inp_MAX_PEOPLE IN VARCHAR2,
    RESPONE OUT BOOLEAN
)
IS
BEGIN
    UPDATE nhom10.ROOM
    SET
    NAME = inp_NAME,
    PRICE = TO_NUMBER(inp_PRICE),
    PRESET_MONEY = TO_NUMBER(inp_PRESET_MONEY),
    STATUS = inp_STATUS,
    TYPE_ROOM = inp_TYPE_ROOM,
    MAX_PEOPLE = inp_MAX_PEOPLE
    WHERE ID = TO_NUMBER(inp_ID);
    
    RESPONE := TRUE;
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := FALSE;
END;

=================================== GRANT ROLE ===================================
CREATE ROLE BASIC_CHAOPHONG;
GRANT SELECT ON nhom10.ROOM TO BASIC_CHAOPHONG;
GRANT SELECT ON nhom10.STAFF TO BASIC_CHAOPHONG;
GRANT SELECT ON nhom10.CUSTOMER TO BASIC_CHAOPHONG;

CREATE ROLE BASIC_LETAN;
GRANT SELECT, INSERT, UPDATE ON nhom10.BOOKING TO BASIC_LETAN;
GRANT SELECT ON nhom10.ROOM TO BASIC_LETAN;
GRANT SELECT ON nhom10.STAFF TO BASIC_LETAN;
GRANT SELECT, INSERT, UPDATE ON nhom10.CUSTOMER TO BASIC_LETAN;

CREATE ROLE BASIC_KETOAN;
GRANT SELECT, INSERT ON nhom10.BILL TO BASIC_KETOAN;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.BILL_DETAILS TO BASIC_KETOAN;
GRANT SELECT, INSERT ON nhom10.BOOKING TO BASIC_KETOAN;
GRANT SELECT ON nhom10.ROOM TO BASIC_KETOAN;
GRANT SELECT ON nhom10.STAFF TO BASIC_KETOAN;
GRANT SELECT, INSERT, UPDATE ON nhom10.CUSTOMER TO BASIC_KETOAN;

CREATE ROLE BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.ACCOUNT TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE ON nhom10.BILL TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE ON nhom10.BILL_DETAILS TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE ON nhom10.BOOKING TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.CUSTOMER TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.ROOM TO BASIC_QUANLY;
GRANT SELECT, INSERT, UPDATE, DELETE ON nhom10.STAFF TO BASIC_QUANLY;

GRANT CONNECT TO BASIC_CHAOPHONG, BASIC_LETAN, BASIC_KETOAN, BASIC_QUANLY;

delete from nhom10.STAFF
select * from nhom10.STAFF

CREATE OR REPLACE PROCEDURE GRANT_PRIVILEGES_ROLES_SCHEMA_NHOM10(
    USERNAME IN VARCHAR2,
    NEW_POSITION IN NVARCHAR2,
    RESPONE OUT VARCHAR2
)
IS
BEGIN
    IF UPPER(NEW_POSITION) = N'CHÀO PHÒNG' 
    THEN 
        EXECUTE IMMEDIATE 'GRANT BASIC_CHAOPHONG TO ' || USERNAME;
    END IF;

    IF UPPER(NEW_POSITION) = N'L? TÂN' 
    THEN 
        EXECUTE IMMEDIATE 'GRANT BASIC_LETAN TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON SYS.DBMS_CRYPTO TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON SYS.DBMS_CRYPTO_FFI TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.SYMMETRIC_DECRYPT TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.SYMMETRIC_ENCRYPT TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.INSERT_CUSTOMER TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.UPDATE_CUSTOMER TO ' || USERNAME;
    END IF;

    IF UPPER(NEW_POSITION) = N'K? TOÁN' 
    THEN
        EXECUTE IMMEDIATE 'GRANT BASIC_KETOAN TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON SYS.DBMS_CRYPTO TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON SYS.DBMS_CRYPTO_FFI TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.SYMMETRIC_DECRYPT TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.SYMMETRIC_ENCRYPT TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.INSERT_CUSTOMER TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.UPDATE_CUSTOMER TO ' || USERNAME;
    END IF;

    IF UPPER(NEW_POSITION) = N'QU?N LÝ' 
    THEN
        EXECUTE IMMEDIATE 'GRANT BASIC_QUANLY TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON SYS.DBMS_CRYPTO TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON SYS.DBMS_CRYPTO_FFI TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.INSERT_STAFF TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.UPDATE_STAFF TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.INSERT_CUSTOMER TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.UPDATE_CUSTOMER TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.HASH_VALUE TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.RSA_DECRYPT TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.RSA_ENCRYPT TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.SIGN_DATA TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.VERIFY_DATA TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.SYMMETRIC_DECRYPT TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT EXECUTE ON nhom10.SYMMETRIC_ENCRYPT TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT CONNECT, ALTER ANY TABLE TO ' || USERNAME;
        EXECUTE IMMEDIATE 'GRANT GRANT ANY ROLE TO ' || USERNAME;
    END IF;
    
    RESPONE := 'TRUE';
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := 'FALSE';
END;


create user test identified by 123;

SET SERVEROUTPUT ON;
DECLARE
    RESPONE VARCHAR2(50);
BEGIN
    REVOKE_PRIVILEGES_ROLES_SCHEMA_NHOM10('test', N'Qu?n lý', RESPONE);
    DBMS_OUTPUT.PUT_LINE(RESPONE);
END;


CREATE OR REPLACE PROCEDURE REVOKE_PRIVILEGES_ROLES_SCHEMA_NHOM10(
    USERNAME IN VARCHAR2,
    OLD_POSITION IN NVARCHAR2,
    RESPONE OUT VARCHAR2
)
IS
BEGIN
     IF UPPER(OLD_POSITION) = N'CHÀO PHÒNG' 
    THEN 
        EXECUTE IMMEDIATE 'REVOKE BASIC_CHAOPHONG FROM ' || USERNAME;
    END IF;

    IF UPPER(OLD_POSITION) = N'L? TÂN' 
    THEN 
        EXECUTE IMMEDIATE 'REVOKE BASIC_LETAN FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.SYMMETRIC_DECRYPT FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.SYMMETRIC_ENCRYPT FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON SYS.DBMS_CRYPTO FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON SYS.DBMS_CRYPTO_FFI FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.INSERT_CUSTOMER FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.UPDATE_CUSTOMER FROM ' || USERNAME;
    END IF;

    IF UPPER(OLD_POSITION) = N'K? TOÁN' 
    THEN
        EXECUTE IMMEDIATE 'REVOKE BASIC_KETOAN FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.SYMMETRIC_DECRYPT FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.SYMMETRIC_ENCRYPT FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON SYS.DBMS_CRYPTO FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON SYS.DBMS_CRYPTO_FFI FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.INSERT_CUSTOMER FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.UPDATE_CUSTOMER FROM ' || USERNAME;
    END IF;

    IF UPPER(OLD_POSITION) = N'QU?N LÝ' 
    THEN
        EXECUTE IMMEDIATE 'REVOKE BASIC_QUANLY FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.INSERT_STAFF FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.UPDATE_STAFF FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.INSERT_CUSTOMER FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.UPDATE_CUSTOMER FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.HASH_VALUE FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.RSA_DECRYPT FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.RSA_ENCRYPT FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.SIGN_DATA FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.VERIFY_DATA FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.SYMMETRIC_DECRYPT FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON nhom10.SYMMETRIC_ENCRYPT FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON SYS.DBMS_CRYPTO FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE EXECUTE ON SYS.DBMS_CRYPTO_FFI FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE ALTER ANY TABLE FROM ' || USERNAME;
        EXECUTE IMMEDIATE 'REVOKE GRANT ANY ROLE FROM ' || USERNAME;
    END IF;
     
    RESPONE := 'TRUE';
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := 'FALSE';
END;


select * from nhom10.STAFF

=================================== SECURITY =========================================

grant execute on SYS.DBMS_CRYPTO to nhom10; 
grant execute on SYS.DBMS_CRYPTO_FFI to nhom10;


// ------------------------- MA HOA -------------------------

CREATE OR REPLACE PROCEDURE SYMMETRIC_ENCRYPT(
    p_input IN VARCHAR2,
    p_key IN VARCHAR2,
    enc_res OUT VARCHAR2
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
    
    enc_res := TO_CHAR(l_raw);
    
    EXCEPTION 
        WHEN OTHERS THEN 
            enc_res := NULL;
END;

// ------------------------- GIAI MA -------------------------

CREATE OR REPLACE PROCEDURE SYMMETRIC_DECRYPT
(
    p_input IN VARCHAR2, 
    p_key IN VARCHAR2,
    dec_res OUT VARCHAR2
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
    
    dec_res := UTL_RAW.CAST_TO_VARCHAR2(l_raw);

    EXCEPTION 
        WHEN OTHERS THEN 
            dec_res := NULL;          
END;


---------------------------- TEST ----------------------------
// ------------------------- MA HOA -------------------------------

SET SERVEROUTPUT ON;
DECLARE
    Result_ENCRYPT_to varchar2(2000);
BEGIN 
    SYMMETRIC_ENCRYPT('<RSAKeyValue><Modulus>yFugw2tEfzizubgUr/CvVhOGb8UodknGU4mvAm8II9+JccVF3cCBdQbOefoTbx5wTn2P3JpU3nY5UlS0v3ut6DgHqSfUKQsXjJg0eA2oWMgj2ITB/FSIrdGLSMWPj3/XOWC0UMC3/ComPgZDZ5028irMvubP3MyJaQpjMqufWn0=</Modulus><Exponent>AQAB</Exponent><P>y3K+Ja2UU4P2NSvc3/dx1KHTUHWreKU4CigzVEYiJgMpEKo1iWtkAFEZ5EJAU46zu9fIkhFIvQfqDazS/QK6Xw==</P><Q>/ByJM8AYabmtuU7XPFTsQcYXiE/XVD/c+rCiEzENJySUTE4A37rdnpMXDIYjihZpAcn58q5pATyG3ExN83tQow==</Q><DP>wecEb2i2m7qD6D178EIoTZ5GhsL/wKbHeHbEJSgK1+vfMbDoAadG0j55zOGP6ZNyni+VTBIJH5DxdEMpfFcjRQ==</DP><DQ>s+/OARCOnA75LfRWbGoUQZGXxCNwBWKbXpVo6BevUouqCJf3ybb+bCqBXd/zR6BdC/jTG2Fd8pL3kg4n6KrPzw==</DQ><InverseQ>TU13ozYSwEN0hOqidxpnZjPEZN2cwl7oJsXx8PBmin0amA5b1D2VacBUxzRTsQ6Ed+x6jmvpNxVGzBByBCbBiQ==</InverseQ><D>iWkmGcBwqtwljwLr1Tq51cvwUKmkNXQle9ea2o1xxMCjkA6e+xXy82LmqwtS1svGdF5zEvYZrLTiXU/Q4t6dBsUuGs4mxavS/lmhGPxoe8f3vmpM7sCS99GztaHcD+XTKreZA2v5eznU5ApOYuS4zOphJ9TNh+nybUY12NFBaDU=</D></RSAKeyValue>', 'b6aef28ccf6b85f3879d39623da0423e', Result_ENCRYPT_to);
    DBMS_OUTPUT.PUT_LINE('RESULT ENCRYPT: ' || Result_ENCRYPT_to);
END;

---------------------------- TEST -------------------------;
// ------------------------- GIAI MA -------------------------------
    
set serveroutput on;
declare
    result_1 varchar(2000);
begin
    SYMMETRIC_DECRYPT('F92940D2764FA2B9F8E3263976F50861741FB8783195EA8D7CD744365DE5182ABA2CC8728B88D691711838E5BF1E355B216F46D6685B75D7223B724F461BDA476C7603DDF3270F8BD08C5713D8A10EB72AA3CF34A091DF8C86F9003CF5182ACB13EDF18AA83B8821F6B39DB95CC5B5F52DC403718E147C2D9DA88FDDF3C353F19A2A166DB767F798A67F036BE1A15A5E0E8C273822964FC2E8BA10FA87B6BB3EF6DB79690775EBEC0E0C8BE8C9737DB24FA685259F4CC30B4F9B9C34EDB5641101A36B9B11EF5919E0B5016E4596B5B4E95AC9060668A42132E38C24E390A681C8E82A16DE7A61E1369DEC19CFC367399D109D30A821FEDB3871F5C418A8F0DBF70C996E732F908C61A9F5139694550AACDB54D9AA73CCE16BFD5C8887BAC8A335A30DC3CD8BA79AB017FFE34B603EEEC7940E2555FBA1F50A55B4C3F3AF562F0BDCB2C5787BC91C4FEE7F240FCFD0B52ECFEC0BB1133EBD72C9FC681861D7E25E6D2FB53EFD35149D28AEDD3479739233322EF3A6C03040C7141681E8BD5675ABB6B4C2876296E940F9AC85D65AB27BC834401FF6A897ED4F1460A98D5DBE28CE3F2B881B2161505E01EB1E71DBDAFC6BCBF27AB8FB11F446C160B80C5F193D3CDBEECCD6D2A6383C70D134D384179ABCDC3F99F733C8665B53BD011147756A5EF4A5C02DC90C1430F04F6833C4F1FD82283AE066005B14506033E8CB63D81C2F8F18A941ADBA9B065C5B7BAF652FF24DCB5EE13B9FD30B817C2145971D580DD3A1B4802D4E008232D712BA4E28232D8EADCE67BF7A47A959270D21D0BB956E8B63012C612EA06DB3E28392DD2A6D5896F4003A26FD7ED9A1A9D1D80789C093E968845E7E2E522CF08D5ECBFA01F645F2E929055716004618A96019B562532D0709FD487AC98E5B7BE01A375E741E3378468F233D32AA6C52881E09B769F78C213F9C8DB400BE3184D7E1BDB35A7A81945DB844EDF2BB71DEE03E3127898AF36C2B2A05AA2911CEDB9ECE6C0AC0957AA2658E5E0C0053B9560EBD71BADAD7C3F5FC0DDA7B3612A415C60A097F617B315AEB3C94BDED44A97344F8FF806A4549F1869A689B81C067893A9C7D94FA4E09CD183D4F39FDBFC57CB1923BC5E94BE454DFFC75A5F6E6D4AECAFAF89EAEA541810F76733CFA9047A49E4E94E864A01B2B940A5CB88675939CC2E8806775940D3A93CD8E2256E2719C93401E55CB2734C4766B76817BCFE6E03A06609F413C120AF8ACB49B9F2F16BA86EC03E90A37BDB0F5315A366A9A5A8D71996D61A39ADBC5B70ED76099B2B6', 'b6aef28ccf6b85f3879d39623da0423e', result_1);
    DBMS_OUTPUT.PUT_LINE(result_1);
end;


----------------------------------------------------------------------
========================= RSA ========================================
----------------------------------------------------------------------

  -- Use OpenSSL to generate the private and public key (2048 bit RSA key)
  -- openssl genrsa -out private.pem 2048
  -- openssl rsa -in private.pem -outform PEM -pubout -out public.pem

CREATE OR REPLACE PROCEDURE RSA_ENCRYPT(
    inp_str IN VARCHAR2,
    public_key IN VARCHAR2,
    enc_res OUT VARCHAR2
)
IS
    eType      PLS_INTEGER := DBMS_CRYPTO.PKENCRYPT_RSA_PKCS1_OAEP;
    kType      PLS_INTEGER := DBMS_CRYPTO.KEY_TYPE_RSA;
    enc_raw    RAW(2048);
BEGIN
    enc_raw := DBMS_CRYPTO.PKENCRYPT
    (
        src        => UTL_RAW.CAST_TO_RAW(inp_str),
        pub_key    => UTL_RAW.CAST_TO_RAW(public_key),
        pubkey_alg => kType,
        enc_alg    => eType
    );

    enc_res := TO_CHAR(enc_raw);
    
    EXCEPTION
        WHEN OTHERS THEN
            enc_res := NULL;
END;


CREATE OR REPLACE PROCEDURE RSA_DECRYPT(
    inp_str IN VARCHAR2,
    private_key IN VARCHAR2,
    dec_res OUT VARCHAR2
)
IS
    eType      PLS_INTEGER := DBMS_CRYPTO.PKENCRYPT_RSA_PKCS1_OAEP;
    kType      PLS_INTEGER := DBMS_CRYPTO.KEY_TYPE_RSA;
    inp_raw     RAW(2048);
    dec_raw     RAW(2048);
BEGIN
    DBMS_OUTPUT.PUT_LINE(private_key);
    SELECT HEXTORAW(inp_str) INTO inp_raw FROM DUAL;
    dec_raw := DBMS_CRYPTO.PKDECRYPT
    (
        src        => inp_raw,
        prv_key    => UTL_RAW.CAST_TO_RAW(private_key),
        pubkey_alg => kType,
        enc_alg    => eType
    );
  
  dec_res := UTL_RAW.CAST_TO_VARCHAR2(dec_raw);
  
   EXCEPTION
        WHEN OTHERS THEN
            dec_res := NULL;
END;


SET SERVEROUTPUT ON;
DECLARE
    inp_str VARCHAR2(2000) := '7AD1205BBEF137F4A60C2AF93A46263C3B008544A0C6DB1FFF9565C6B9FB2E35';
    public_key VARCHAR2(2000) := 'MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtJYgVtlyGP+GMUaqS9E4yKKvLgReNph+2njjjyJYejEmkuQDQCfb47NDENzdoJKQeoTZxW7ARZqgCM8ghNdLEZBN2a8Qh706DtuXDHE+uQVmOOrmpH5N2RjWdIsqf1B0kO0ZyPRzsOdqTWkrS6Gd1pYM30pQNEdgrVX/PNG+okboPstnWOzKFFg1jjMg2WOSZP19Clf3gulzY9dC9zkzXTF50xZmFD5SiAKXZ06eLwDi0np7/FxEqX1P2mY2/aN8kzJ7tQ+C5YRXCmS4DZ+L0VIFrNmpcY9MbujABpuKBh0FKQvkj8+2C8KdnGriYz29IYxhE5ZkdmgI30l2eZnXFQIDAQAB';
    enc_res VARCHAR2(2048);
BEGIN
    RSA_ENCRYPT(inp_str, public_key, enc_res);
    DBMS_OUTPUT.PUT_LINE(enc_res);
END;
    

SET SERVEROUTPUT ON;
DECLARE
    inp_str VARCHAR2(2000) := '80A1C6F15E43B4E49CD0302D733DE54615195AFA33F3319CC62BF2F2580ECF7867254BABF29229BACD9D7A3333825C6F116569E3489599BA887AA8F7AEF75E1179D45758BD84E7F742A9A1CF058604A72BB0C2F62A595F31428855D580C0CE184D2A8BF9C76CC1A1064B84EF7B381AB2727BB63E8FA38C26DE3BE321B4EFBB95CA6DAC5414D46829EC03F1388DD8E6F9E75FA3B9B563233E661528355D71D86BE61DF69F994EF38ACE85995DE5E0D9C4C6AA1609FCA2EC86226DBD4E4DA95FF8AC71709880FBF27502738D583B2E6FF0D2B1EA2F28B737225B91413BA2639CD0D93A0F00D3AFF67D43643A20FDBBDD3DF5F36C29A90FEFFFADF251B0ECF23A95';
    private_key VARCHAR2(4096) := 'MIIEpAIBAAKCAQEAtJYgVtlyGP+GMUaqS9E4yKKvLgReNph+2njjjyJYejEmkuQDQCfb47NDENzdoJKQeoTZxW7ARZqgCM8ghNdLEZBN2a8Qh706DtuXDHE+uQVmOOrmpH5N2RjWdIsqf1B0kO0ZyPRzsOdqTWkrS6Gd1pYM30pQNEdgrVX/PNG+okboPstnWOzKFFg1jjMg2WOSZP19Clf3gulzY9dC9zkzXTF50xZmFD5SiAKXZ06eLwDi0np7/FxEqX1P2mY2/aN8kzJ7tQ+C5YRXCmS4DZ+L0VIFrNmpcY9MbujABpuKBh0FKQvkj8+2C8KdnGriYz29IYxhE5ZkdmgI30l2eZnXFQIDAQABAoIBAQCz7j1sq54ewELhyCoX/vAYINhw/lMtDMagQgFidXM41M49X8jJXK2gc9Wn8Jk2y7H/EW22ZUJYV9eKz230TltytZiMA/1xch5t/WfEMvWu/m062vpR/bLtU+0iFCOU1QJAAK5HZyH6qqllVhuYcQDGdZ1/whil7lSqgNweqlHKdzOtVtTfL6AEGLd4QZlFp4alH5qTQACme7pnUGKnjo+xbS824O3Qq6hD/nLQPGqMdD6vYv4fvURDNChPudUesrl46DPJQ4uUy2DbOpax7IrWZInPho7cLTuK+ifh0X5wxyF01GqYeP3cMIhc35BMahmPa4s5qXCrUmXksScXZSxlAoGBAOzAv/svNusc2w9+Lz9By2OUSgzNgSMhByZjzvNAdUig9O0tkQ+4dxkNeDuWb4O6St6YU2t8sdHl4Fi+rVGYzfclSYpZPc5INfGvnvnB+DjYcLSR03i0o5y/5abelSXf72VK5jecMsFBjQ3Z39o8VxMrCblnsBCFKxf6IrSMf7cnAoGBAMNEc5hko+4n1CsOqpBgJw7JZm4/JGJHw73ScFAHHRYJVgyc7TzakSF2qAx+wNmp22y4QiIMLiSu21RUI/uv8TXkpTDxN68t71SxvdAHjwu7/pL88/t+5Svu1HsjwxGQc3BEHjScUIy4rvGUsqmrc/rQ1629T8g9JCS5CuBUs8VjAoGBAIs3GGzSqZ99G08z6wJuZPPMOfLnUL1gaXzoicO+LnkjtIJXHgq5dMb1hbK8VhyGign0VnFJJKMBYnesv+vQR6TBd0n0CgTnr1jL5VaF+bkrhLKZYVmEFwVQTfFexyM7V+EfNtqSbcH4EsKHUDta6fTqhNBSKVS9icaSnp++UkdXAoGAd91/MB8nWjxsy3mXTkUX9MXA8RAln4b6K34Qn6+eNTbJ8bgrEV4CFQ6nnfP5IiRYo+aAAjKD6NTFooB8DjY01aZjmSWZzxldfxoUsKzTLJrcbRKGGu4Mc+mJ2YVca13G3zWRbS4/bAK6sWVcPzcWXPLyUp6RaRPPKhPFD2wvZo0CgYBbyBTLisnMEui2mCmbbJp/fN6PtsunEGWqiaugfRHKlcT9rZyPyvvcTwlDI1sl5NTWsGcqaf1Rq+rDXnJ9sj3M8Nk0X/mjbnf6qmr8Xb3jd9onfYRrBof9VfkeuOAZIi/BK8WkpsoPn+am2c3IHj7j+HSI53o9aYn5L+w9AN0Q6g==';
    dec_res VARCHAR2(2048);
BEGIN
    RSA_DECRYPT(inp_str, private_key, dec_res);
    DBMS_OUTPUT.PUT_LINE(dec_res);
END;


=============================== SIGN DATA ==========================================

  -- Use OpenSSL to generate the private and public key (2048 bit RSA key)
  -- openssl genrsa -out private.pem 2048
  -- openssl rsa -in private.pem -outform PEM -pubout -out public.pem


CREATE OR REPLACE PROCEDURE SIGN_DATA(
    inp_str  IN  VARCHAR2,
    private_key IN VARCHAR2,
    sign_res OUT VARCHAR2
)
IS
    sType      PLS_INTEGER := DBMS_CRYPTO.SIGN_SHA224_RSA;
    kType      PLS_INTEGER := DBMS_CRYPTO.KEY_TYPE_RSA;
    sign_raw   RAW (2048);
BEGIN
    sign_raw := DBMS_CRYPTO.SIGN
    (
        src        => UTL_RAW.CAST_TO_RAW(inp_str),
        prv_key    => UTL_RAW.CAST_TO_RAW(private_key),
        pubkey_alg => kType,
        sign_alg   => sType
    );
  
    sign_res := TO_CHAR(sign_raw);
  
    EXCEPTION
        WHEN OTHERS THEN
            sign_res := NULL;
END;


DECLARE
    inp_str VARCHAR2(50) := 'tui test thu nha';
     private_key VARCHAR2(2000) := 'MIIEowIBAAKCAQEA5U31HO2k9U5Q5SB99BFzcPJ8FX85rA58hEU3ZH5zuc1GPbaWEEv6x/4XPLNSJRyWYVk8fBGAKPfGpXfCOGagSQ8DV4++3mshjZkbXFY+yt498U4zXHMr1r+k2hb2XHJgN94/SJwWOUF+sUTGfyPzzI59KuT5IWyyyAGNNYzPsrsrVq2Z
                                OCCOhlluxem4rJJg66BtqYSxIypmiVCWEnhY6Knb5UNuRoduLEqGCBF/y7jT9Gsui5IETicMnFJQiirblCeBhxGHYumBM1syoxJAaUiGrh8EEniQAkyJj13sGWwlcmWwq3vreHJd8WDifSkwenLSMb+jmesCKo5nMaE/4wIDAQABAoIBAQDXPTcAoX1/GbVL
                                bhVsnVjcQ3EACL1M6QgubH5TYXMljC8LzLDNuVQ8mCCdxMEtvsjVthrVZuQDEJmxRlnT8VkxWttLPM3wH2WOcZJCOV6VtMk2Ea6acC1NVfTbFkTIgEAbEQ4cDQ+7TOQsZ59fRpMZhwKs2eOUYWDr5rDy/CsV213O36MrNqko9KvTgj69ZzPcoVXrtPcPyWHO
                                BQthJ+nmOotZa6Y0mEaC7jlKFwybJ66vfp9WS7uphqBi4KKfSM2Jsb5Du5NT1KxTr0YqviRTktPLNU8ZZu5w++yvf0A3Ohqs6JxzYctTfjyHCrslI64XJ98I/dYNThYhapJRHlppAoGBAPzF16l4jn81KrovDx7VuaUe9SuLInvcR5d5cgKqoTyeYSsWCoyl
                                AkVuRBQrWe8Xtof7iJ8MX0rHkHuVnwvIDS7i9XjZvlPMFiHRyoLXOfmqYROKj8pQ3rbi5+Le/rJyEjHGZ4UKe6flWMXmAjNWhbkb/mPzSWN8+FZr0FQLMa+9AoGBAOg7aWbKmm9ufPMvYQ5vriks9LBfO2QcZDxfkfdKx5aLcS3emlkoxx4iRpBBkHLpFTpV
                                0kgZZGEbNpMeBm3Psna+xelG5Dx1rgGZ8Be+vkicecxTtQCCJJMudMLwdWYLzJVl9WaXsNs6q8PK51uio1YZTdt7j5XOGsCN7g2kRFgfAoGAR/KQRh6YgMDDXqdSaHZxFvzO0AwUTqkOf6EDwJqMtlJmWfs2GX5GPTj7i7ojKRjYza/c3ViKLyDKkUKvOI1C
                                o1vafwGLWRK4Ifwy5jcYu2Wxp/xCnVWTbv76/ep3GJe7cguFH6syM0HmmL21VqOEAIJlUFHJS9YgYYjijWl6RQkCgYBfUFMYPztXVM+vt9hr5mZiu/LJmKx1kDmLleYAyw5TuXOAUgajZVskAQlZF6/Dmep+gM7HjLRoLpUdmIsm8sHafr4X2mK+dcYvHhEu
                                jbjncGGE/S4iwUdlqQF/KNXmRh13i9tGeie6MsBgoZyHPZ8wLT6JQJNT9r2AHkCAFNWAHQKBgC3KYVpv57GwGzPTFGmXjxeFUtcsVl9bP0AQGP4L5xjYpXt63Kc1xPPuSP25CNyHK2QiAlCyHDxD75UYq4X0BQiNEcOEQyXIvRCBAoGtilQbN7+5AUX1dFYz
                                S3K4E6MnTBYfIQxIbYKlUpoT0cTyCiv/iRbkJO9rn8XeiqJ3SKOs';
    sign_res VARCHAR2(2000);
BEGIN
    SIGN_DATA(inp_str, private_key, sign_res);
    
    DBMS_OUTPUT.PUT_LINE(sign_res);
END;


CREATE OR REPLACE PROCEDURE VERIFY_DATA(
    inp_str IN VARCHAR2,
    inp_sign_val  IN  VARCHAR2,
    public_key IN VARCHAR2,
    verify_return OUT BOOLEAN
)
IS
    sType      PLS_INTEGER := DBMS_CRYPTO.SIGN_SHA224_RSA;
    kType      PLS_INTEGER := DBMS_CRYPTO.KEY_TYPE_RSA;
    inp_sign_raw     RAW(2048);
    sign_raw   RAW (2048);
BEGIN
    
    SELECT HEXTORAW(inp_sign_val) INTO inp_sign_raw FROM DUAL;
    verify_return := DBMS_CRYPTO.VERIFY
    (
        src        => UTL_RAW.CAST_TO_RAW(inp_str),
        sign       => inp_sign_raw,
        pub_key    => UTL_RAW.CAST_TO_RAW(public_key),
        pubkey_alg => kType,
        sign_alg   => sType
    );
    
    EXCEPTION
        WHEN OTHERS THEN
            verify_return := FALSE;
END;

DECLARE
     inp_sign_val VARCHAR2(2000) := 'C99C660222FDA6E30AF247BAC71BB022F1F3E4F53B3A0E4AA3B97829710BF898C6984360815C9C4ED38655665B1DE0B3AE47D0918421CA22A3A3CB3E01827F27411F1E13CDA849AF62A71E20A52F92AE37634D88873414E5FC9220B9ADCEA17F2FE56ED838302E1057A43DBD6DC78B3DF454A9244B07C60D35F8A79B14AFFD9340F705ED6ACB703CC3A097FFB8B41B096D9D91D631A4468CC30684A0FFF29ACC293C2E43DBF3B035588DBF2A7D2CEA7D478D718C006ED24FA79BBB2DBE0F3AC3B93ED86C938C03CAC6DEBA2EF52B5C56A824B63CAE01CE7470E48CDF31B6B16D2C76D3FA678C4A703C7CE28AD7BC15C4639A0CB773BCFB088654F22A7F20A5F5';
     public_key VARCHAR2(2000) := 'MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA5U31HO2k9U5Q5SB99BFz
                                    cPJ8FX85rA58hEU3ZH5zuc1GPbaWEEv6x/4XPLNSJRyWYVk8fBGAKPfGpXfCOGag
                                    SQ8DV4++3mshjZkbXFY+yt498U4zXHMr1r+k2hb2XHJgN94/SJwWOUF+sUTGfyPz
                                    zI59KuT5IWyyyAGNNYzPsrsrVq2ZOCCOhlluxem4rJJg66BtqYSxIypmiVCWEnhY
                                    6Knb5UNuRoduLEqGCBF/y7jT9Gsui5IETicMnFJQiirblCeBhxGHYumBM1syoxJA
                                    aUiGrh8EEniQAkyJj13sGWwlcmWwq3vreHJd8WDifSkwenLSMb+jmesCKo5nMaE/
                                    4wIDAQAB';
                                    
    inp_str VARCHAR2(2000) := 'tui test thu nha';
    verify_return BOOLEAN;
BEGIN
    VERIFY_DATA(inp_str, inp_sign_val, public_key, verify_return);
    
    if verify_return then DBMS_OUTPUT.PUT_LINE('TRUE');
    else DBMS_OUTPUT.PUT_LINE('FALSE'); end if;
END;

========================================= HASH ==========================================
CREATE OR REPLACE PROCEDURE HASH_VALUE(
    inp_str IN VARCHAR2,
    out_hash OUT VARCHAR2
)
IS
BEGIN
    SELECT DBMS_CRYPTO.HASH(
        UTL_RAW.CAST_TO_RAW(inp_str),
        DBMS_CRYPTO.HASH_SH256
    ) INTO out_hash FROM DUAL;
    
    EXCEPTION
        WHEN OTHERS THEN
            out_hash := NULL;
END;

SET SERVEROUTPUT ON;
DECLARE
    inp_str VARCHAR2(2000) := 'C6145240A3B5A1260E3FBF5A376270BD9A8B1CBF6071FD76888228835FBDD64CABFDA5E4B82E921FC0BB63CA8A7C068EBBA405A566F7470567629D8177EE079A67B4BDA809A2DA8E5CF7B6672B30072069BEEC9F68529FBBC5C61BA5E9D84CAD09F8F8F3219B7545875CF7ADD65C92A961748AA2DF168C2E3B3FB645FBC7F517F23832EC9691FE707ABAF6DBC0306B5AB88FCA12542E83BEC51FE5C162CA006B78A17C2E736CE33604AD7E0DC462B7D96C72B5BDBDB2DF084C3ECA2DE46D282F40C28EF30177F37E96177491F98F5EA3679B9C5E8300388FB061E415DF308FF305F4622613088D988335BD3E6E2D119C85246D7D6CFD07D146BCA9878D709AAD';
    out_hash VARCHAR2(2000);
BEGIN
    HASH_VALUE(inp_str, out_hash);
    DBMS_OUTPUT.PUT_LINE(out_hash);
END;


// PROCEDURE SET LEVEL OLS FOR USER OF LBACSYS

CREATE OR REPLACE PROCEDURE SET_LEVEL_OLS_SCHEMA_NHOM10_FOR_USER(
    USERNAME    IN VARCHAR2,
    POSITION    IN NVARCHAR2,
    RESPONE     OUT VARCHAR2
)
IS
BEGIN
    IF UPPER(POSITION) = N'CHÀO PHÒNG'
    THEN
        EXECUTE IMMEDIATE 'BEGIN SA_USER_ADMIN.SET_LEVELS(policy_name => ''ChinhSach_OLS_STAFF_Schema_nhom10'', user_name => '''|| 
                USERNAME || ''', max_level => ''CP'', min_level => ''CP'', def_level => ''CP'', row_level => ''CP''); END;';
    END IF;

    IF UPPER(POSITION) = N'L? TÂN'
    THEN
        EXECUTE IMMEDIATE 'BEGIN SA_USER_ADMIN.SET_LEVELS(policy_name => ''ChinhSach_OLS_STAFF_Schema_nhom10'', user_name => '''|| 
                USERNAME || ''', max_level => ''LT'', min_level => ''CP'', def_level => ''LT'', row_level => ''LT''); END;';
    END IF;
    
    IF UPPER(POSITION) = N'K? TOÁN'
    THEN
        EXECUTE IMMEDIATE 'BEGIN SA_USER_ADMIN.SET_LEVELS(policy_name => ''ChinhSach_OLS_STAFF_Schema_nhom10'', user_name => '''|| 
                USERNAME || ''', max_level => ''KT'', min_level => ''CP'', def_level => ''KT'', row_level => ''KT''); END;';
    END IF;
    
    IF UPPER(POSITION) = N'QU?N LÝ'
    THEN
        EXECUTE IMMEDIATE 'BEGIN SA_USER_ADMIN.SET_LEVELS(policy_name => ''ChinhSach_OLS_STAFF_Schema_nhom10'', user_name => '''|| 
                USERNAME || ''', max_level => ''QL'', min_level => ''CP'', def_level => ''QL'', row_level => ''QL''); END;';
    END IF;
    
    RESPONE := 'TRUE';
    
    EXCEPTION
        WHEN OTHERS THEN
            RESPONE := 'FALSE';
END;


--------------------------------------------------------
============ ORACLE LABEL SECURITY =====================
--------------------------------------------------------
col object_name format a30

alter user lbacsys identified by 123 account unlock container = all;

grant create procedure, connect, resource to lbacsys;
grant select, update on nhom10.STAFF to lbacsys;
grant alter on nhom10.staff to lbacsys;
grant execute on sa_sysdba to lbacsys;

EXEC LBACSYS.CONFIGURE_OLS;
--Cho phép c?u hình và qu?n lý nhãn

EXEC LBACSYS.OLS_ENFORCEMENT.ENABLE_OLS;
--Cho phép th?c thi các th? t?c c?a ols

SELECT STATUS FROM DBA_OLS_STATUS WHERE NAME = 'OLS_CONFIGURE_STATUS';

exec sa_sysdba.create_policy(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', column_name => 'Label');

exec sa_components.create_level(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', level_num => 100, short_name => 'QL', long_name => 'Quan_ly');
exec sa_components.create_level(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', level_num => 75, short_name => 'KT', long_name => 'Ke_toan');
exec sa_components.create_level(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', level_num => 50, short_name => 'LT', long_name => 'Le_tan');
exec sa_components.create_level(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', level_num => 25, short_name => 'CP', long_name => 'Chao_phong');

exec sa_label_admin.create_label(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', label_tag => 95, label_value => 'QL', data_label => true);
exec sa_label_admin.create_label(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', label_tag => 65, label_value => 'KT', data_label => true);
exec sa_label_admin.create_label(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', label_tag => 45, label_value => 'LT', data_label => true);
exec sa_label_admin.create_label(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', label_tag => 15, label_value => 'CP', data_label => true);

exec sa_policy_admin.apply_table_policy(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', schema_name => 'nhom10', table_name => 'STAFF', table_options => 'LABEL_DEFAULT, READ_CONTROL, WRITE_CONTROL');

update nhom10.Staff set Label = char_to_label('ChinhSach_OLS_STAFF_Schema_nhom10', 'QL') where upper(Position) = 'QU?N LÝ';
update nhom10.Staff set Label = char_to_label('ChinhSach_OLS_STAFF_Schema_nhom10', 'KT') where upper(Position) = 'K? TOÁN';
update nhom10.Staff set Label = char_to_label('ChinhSach_OLS_STAFF_Schema_nhom10', 'LT') where upper(Position) = 'L? TÂN';
update nhom10.Staff set Label = char_to_label('ChinhSach_OLS_STAFF_Schema_nhom10', 'CP') where upper(Position) = 'CHÀO PHÒNG';

exec sa_user_admin.set_levels(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', user_name => 'nhom10', max_level => 'QL', min_level => 'CP', def_level => 'QL', row_level => 'QL');

grant execute on LBACSYS.LBAC_EVENTS to lbacsys;

alter table nhom10.staff drop column label;
================ DROP POLICY ===================================================
exec sa_sysdba.drop_policy(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10');
================================================================================

