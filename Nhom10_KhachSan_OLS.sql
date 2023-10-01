alter session set "_oracle_script" = true;
create user nam identified by nam;
create user harry identified by harry;
create user jone identified by jone;

create role role_demo;
grant create session, create table to role_demo;
grant role_demo, unlimited tablespace to nam, harry, jone;

grant select, update, delete on nam.NhanVien to nam, harry, jone;

REVOKE create session, create table, unlimited tablespace from harry, jone, tony;
REVOKE role_demo, unlimited tablespace from nam;

drop user harry;
drop user jone;
drop user tony;

drop user nam cascade;

show con_name;

show user;

alter session set container = DEMO_OLS;

create table nam.NhanVien(
	maNV        int,
	tenNV       varchar2(50),
	gioiTinh    varchar2(4),
	sdt         varchar2(11),
	chucVu      varchar2(30),
	constraint PR_NhanVien_maNV primary key(maNV)
);

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (238, 'Nguyen Van A', 'Nam', '0987654321', 'Quan ly');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (2, 'Tran Thi B', 'Nu', '0123456789', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (3, 'Pham Van C', 'Nam', '0943215678', 'Ke toan');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (4, 'Le Thi D', 'Nu', '0912345678', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (5, 'Tran Van E', 'Nam', '0988888888', 'Ke toan');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (6, 'Nguyen Thi F', 'Nu', '0911111111', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (7, 'Le Van G', 'Nam', '0966666666', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (8, 'Tran Thi H', 'Nu', '0933333333', 'Ke toan');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (9, 'Nguyen Van I', 'Nam', '0977777777', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (10, 'Pham Van J', 'Nam', '0910999888', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (11, 'Le Thi K', 'Nu', '0909090909', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (12, 'Tran Van L', 'Nam', '0977788999', 'Ke toan');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (13, 'Nguyen Thi M', 'Nu', '0942222111', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (14, 'Pham Van N', 'Nam', '0977333444', 'Ke toan');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (15, 'Le Thi O', 'Nu', '0969696969', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (16, 'Tran Van P', 'Nam', '0939393939', 'Ke toan');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (17, 'Nguyen Van Q', 'Nam', '0912121212', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (18, 'Pham Thi R', 'Nu', '0999999999', 'Ke toan');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (19, 'Le Van S', 'Nam', '0922222222', 'Nhan vien');

INSERT INTO nam.NhanVien (maNV, tenNV, gioiTinh, sdt, chucVu)
VALUES (20, 'Tran Thi T', 'Nu', '0906060606', 'Ke toan');

alter user lbacsys identified by 123 account unlock;
 
sqlplus;

conn sys/123@//localhost:1521/demo_ols as sysdba;

select * from nam.NhanVien;
EXEC LBACSYS.CONFIGURE_OLS;
--Cho phép c?u hình và qu?n lý nhãn

EXEC LBACSYS.OLS_ENFORCEMENT.ENABLE_OLS;
--Cho phép th?c thi các th? t?c c?a ols

SELECT STATUS FROM DBA_OLS_STATUS WHERE NAME = 'OLS_CONFIGURE_STATUS';


conn lbacsys/123@//localhost:1512/demo_ols;


grant select, update, delete, insert on nam.NhanVien to lbacsys;
grant create procedure to lbacsys;

exec sa_sysdba.create_policy(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', column_name => 'Label');

exec sa_components.create_level(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', level_num => 100, short_name => 'QL', long_name => 'Quan_ly');
exec sa_components.create_level(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', level_num => 75, short_name => 'KT', long_name => 'Ke_toan');
exec sa_components.create_level(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', level_num => 50, short_name => 'NV', long_name => 'Nhan_vien');

exec sa_label_admin.create_label(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', label_tag => 95, label_value => 'QL', data_label => true);
exec sa_label_admin.create_label(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', label_tag => 65, label_value => 'KT', data_label => true);
exec sa_label_admin.create_label(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', label_tag => 45, label_value => 'NV', data_label => true);

exec sa_policy_admin.apply_table_policy(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', schema_name => 'nhom10', table_name => 'STAFF', table_options => 'LABEL_DEFAULT, READ_CONTROL, WRITE_CONTROL');

update nhom10.Staff set Label = char_to_label('ChinhSach_OLS_STAFF_Schema_nhom10', 'QL') where upper(Position) = 'QU?N LÝ';
update nhom10.Staff set Label = char_to_label('ChinhSach_OLS_STAFF_Schema_nhom10', 'KT') where upper(Position) = 'K? TOÁN';
update nhom10.Staff set Label = char_to_label('ChinhSach_OLS_STAFF_Schema_nhom10', 'NV') where upper(Position) = 'NHÂN VIÊN';

select * from nhom10.STAFF;

show user;

exec sa_user_admin.set_levels(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', user_name => 'nhom10', max_level => 'QL', min_level => 'NV', def_level => 'QL', row_level => 'QL');
exec sa_user_admin.set_levels(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', user_name => 'huunam66', max_level => 'KT', min_level => 'NV', def_level => 'KT', row_level => 'KT');
exec sa_user_admin.set_levels(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10', user_name => 'huunam66', max_level => 'NV', min_level => 'NV', def_level => 'NV', row_level => 'NV');

select * from nhom10.STAFF
BEGIN
    FOR i IN 1..11
    LOOP
        IF i < 4
        THEN
            INSERT INTO nhom10.STAFF (Name, Email, CMND, Phone, BirthDay, Gender, Position, Label) 
            VALUES ('Staff ' || i, 'staff'||i||'@email.com', '1234567'||i, '098765431'||i, SYSDATE-i, 'Nam', 'Quan ly', 95);
        ELSIF i < 6
        THEN
            INSERT INTO nhom10.STAFF (Name, Email, CMND, Phone, BirthDay, Gender, Position, Label) 
            VALUES ('Staff ' || i, 'staff'||i||'@email.com', '1234567'||i, '098765421'||i, SYSDATE-i, 'Nam', 'Ke toan', 65);
        ELSE
            INSERT INTO nhom10.STAFF (Name, Email, CMND, Phone, BirthDay, Gender, Position, Label) 
            VALUES ('Staff ' || i, 'staff'||i||'@email.com', '1234567'||i, '098765421'||i, SYSDATE-i, 'Nam', 'Nhan vien', 45);
        END IF;
    END LOOP;
    COMMIT;
END;
/

grant alter on nhom10.staff to lbacsys

alter table nhom10.staff drop column label

set linesize 3000;

exec sa_sysdba.drop_policy(policy_name => 'ChinhSach_OLS_STAFF_Schema_nhom10');