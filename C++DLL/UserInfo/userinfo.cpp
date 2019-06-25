/*
	C/C++����MySQL���ݿ�ʱ����Ҫ����һ��*.h��mysqlͷ�ļ���һ��mysql��lib�ļ�
	1����ʼ����2���������ݿ⣻3��ִ��sql��ѯ��䣻4����ȡ��ѯֵ��5���ر�
*/
#include <stdio.h>
#include <WinSock.h>  
#include <mysql.h> 
#include <Windows.h>
#pragma comment(lib,"wsock32.lib")
#pragma comment(lib,"libmysql.lib")

MYSQL mysql;
MYSQL_FIELD* fd;    //�ֶ�������
char field[32][32]; //���ֶ�����ά����
MYSQL_RES* res;     //�е�һ����ѯ�����
MYSQL_ROW column;   //�����е���
char query[150];    //��ѯ���

//��������
bool ConnectDatabase();
void FreeConnect();
bool QueryDatabase();
bool InsertData();
bool ModifyData();
bool DeleteData();

int main(int argc, char** argv) {
	ConnectDatabase();
	QueryDatabase();
	InsertData();
	QueryDatabase();
	ModifyData();
	QueryDatabase();
	//DeleteData();
	//QueryDatabase();
	FreeConnect();
	system("pause");
	return 0;
}

//�������ݿ�
bool ConnectDatabase() {
	//Gets or initializes a MYSQL structure
	mysql_init(&mysql);

	// Connects to a MySQL server
	const char host[] = "localhost";
	const char user[] = "root";
	const char passwd[] = "root";
	const char db[] = "employees";
	unsigned int port = 3306;
	const char* unix_socket = NULL;
	unsigned long client_flag = 0;

	/*A MYSQL* connection handler if the connection was successful,
	NULL if the connection was unsuccessful. For a successful connection,
	the return value is the same as the value of the first parameter.*/
	if (mysql_real_connect(&mysql, host, user, passwd, db, port, unix_socket, client_flag)) {
		printf("The connection was successful.\n");
		return true;
	}
	else {
		/*const char *mysql_error(MYSQL *mysql)
		Returns the error message for the most recently invoked MySQL function
		A null-terminated character string that describes the error.
		An empty string if no error occurred.*/
		printf("Error connecting to database:%s\n", mysql_error(&mysql));
		return false;
	}
}

//�ͷ���Դ
/*void mysql_free_result(MYSQL_RES *result)
Frees the memory allocated for a result set by mysql_store_result(),
mysql_use_result(), mysql_list_dbs(), and so forth.When you are done
with a result set, you must free the memory it uses by calling mysql_free_result().
Do not attempt to access a result set after freeing it.*/

/*void mysql_close(MYSQL *mysql)
Closes a previously opened connection.mysql_close() also deallocates
the connection handler pointed to by mysql if the handler was allocated
automatically by mysql_init() or mysql_connect().*/
void FreeConnect() {
	mysql_free_result(res);
	mysql_close(&mysql);
}

//��ѯ����
bool QueryDatabase() {
	//�����ݸ�ʽ��������ַ���
	sprintf_s(query, "select * from departments");
	//���ñ����ʽ
	mysql_query(&mysql, "set names gbk");

	/*int mysql_query(MYSQL *mysql, const char *stmt_str)
	Executes an SQL query specified as a null-terminated string
	Executes the SQL statement pointed to by the null-terminated string stmt_str.
	Normally, the string must consist of a single SQL statement without
	a terminating semicolon (;). If multiple-statement execution has been enabled,
	the string can contain several statements separated by semicolons.
	Return Values:Zero for success. Nonzero if an error occurred.*/
	if (mysql_query(&mysql, query)) {
		printf("Query failed (%s)\n", mysql_error(&mysql));
		return false;
	}
	else {
		printf("query success\n");
	}

	/*MYSQL_RES *mysql_store_result(MYSQL *mysql)
	Retrieves a complete result set to the client
	mysql_store_result() reads the entire result of a query to the client,
	allocates a MYSQL_RES structure, and places the result into this structure.
	mysql_store_result() returns a null pointer if the statement did not return
	a result set(for example, if it was an INSERT statement). mysql_store_result()
	also returns a null pointer if reading of the result set failed.
	You can check whether an error occurred by checking whether mysql_error()
	returns a nonempty string. Return Values:A MYSQL_RES result structure with
	the results.NULL(0) if an error occurred.*/
	res = mysql_store_result(&mysql);
	if (!res) {
		printf("Couldn't get result from %s\n", mysql_error(&mysql));
		return false;
	}

	/*my_ulonglong mysql_affected_rows(MYSQL *mysql)
	It returns the number of rows changed, deleted,
	or inserted by the last statement if it was an UPDATE, DELETE, or INSERT.
	For SELECT statements, returns the number of rows in the result set.*/
	printf("number of dataline returned: %d\n", mysql_affected_rows(&mysql));

	/*MYSQL_FIELD *mysql_fetch_field(MYSQL_RES *result)
	Returns the definition of one column of a result set as a MYSQL_FIELD structure.
	Call this function repeatedly to retrieve information about all columns in the result set.*/

	// ��ȡ����
	int j = mysql_num_fields(res);

	//�洢�ֶ���Ϣ
	char* str_field[32];

	//��ȡ�ֶ���
	for (int i = 0; i < j; i++) {
		str_field[i] = mysql_fetch_field(res)->name;
	}

	//��ӡ�ֶ�
	for (int i = 0; i < j; i++)
		printf("%10s\t", str_field[i]);
	printf("\n");

	//��ӡ��ѯ���
	//MYSQL_ROW mysql_fetch_row(MYSQL_RES *result)
	//Fetches the next row from the result set
	while (column = mysql_fetch_row(res)) {
		printf("%10s\t%10s\n", column[0], column[1]);
	}
	return true;
}

//��������
bool InsertData() {
	sprintf_s(query, "insert into departments values ('xxxx', 'xxxxx');");
	if (mysql_query(&mysql, query)) {
		printf("Query failed (%s)\n", mysql_error(&mysql));
		return false;
	}
	else {
		printf("Insert success\n");
		return true;
	}
}

//�޸�����
bool ModifyData() {
	sprintf_s(query, "update departments set dept_name='yyyyy' where dept_no='xxxx'");
	if (mysql_query(&mysql, query)) {
		printf("Query failed (%s)\n", mysql_error(&mysql));
		return false;
	}
	else {
		printf("Insert success\n");
		return true;
	}
}

//ɾ������
bool DeleteData()
{
	sprintf_s(query, "delete from departments where dept_no='xxxx';");
	if (mysql_query(&mysql, query)) {
		printf("Query failed (%s)\n", mysql_error(&mysql));
		return false;
	}
	else {
		printf("Insert success\n");
		return true;
	}
}