#include "pch.h"

#include "CalculateLike.h"

int CalculateLike::Class1::updateMovieRate(int rate, int likes, int score)
{
	float totalRate = rate * likes;
	rate = (totalRate + score) / (likes + 1);
	return rate;
}

int CalculateLike::Class1::updateMovieRate(int rate, int likes, int rateScore, int score)
{
	float totalRate = rate * likes;
	rate = (totalRate - rateScore + score) / (likes);
	return rate;
}
