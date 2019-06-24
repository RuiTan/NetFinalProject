#include "TimeUtil.h"


char* FormatTime(int t) {
	time_t time = t;
	char result[80];
	struct tm* p = new tm();
	gmtime_s(p, &time);
	strftime(result, 80, "%Y-%m-%d %H:%M", p);
	return result;
}