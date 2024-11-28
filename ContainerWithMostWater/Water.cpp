#include<bits/stdc++.h>
using namespace std;

int mostWaterContainer(int arr[], int n)
{
   
    int left = 0;
    int right = n - 1;
    int area = 0;

    while (left < right)
    {
        area = max(area, min(arr[left], arr[right]) * (right - left));

        if (arr[left] < arr[right])
            left += 1;

        else
            right -= 1;
    }
    return area;
}
int main() {
    int n = 9;  
    int arr[] = {1, 8, 6, 2, 5, 4, 8, 3, 7};

    cout << mostWaterContainer(arr, n) << endl;
    return 0;
}
