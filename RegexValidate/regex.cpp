/*
 * regex.cpp - 用正则表达式验证电子邮件地址
 *
 *  C++11标准  STL正则表达式
 *
 */
#include "regex.h"

using namespace std;

bool RegexValidate(char* email_address)
{
	regex pattern("([0-9A-Za-z\\-_\\.]+)@([0-9a-z]+\\.[a-z]{2,3}(\\.[a-z]{2})?)");
	// 正则表达式，匹配规则：
	// 第1组（即用户名），匹配规则：0至9、A至Z、a至z、下划线、点、连字符之中
	// 的任意字符，重复一遍或以上
	// 中间，一个“@”符号
	// 第二组（即域名），匹配规则：0至9或a至z之中的任意字符重复一遍或以上，
	// 接着一个点，接着a至z之中的任意字符重复2至3遍（如com或cn等），
	// 第二组内部的一组，一个点，接着a至z之中的任意字符重复2遍（如cn或fr等）
	// 内部一整组重复零次或一次
	if (regex_match(email_address, pattern))
		return true;
	else
		return false;
}
