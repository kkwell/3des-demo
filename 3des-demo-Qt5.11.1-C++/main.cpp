#include <QCoreApplication>
#include <QDebug>
#include "des.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    QByteArray key = "YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4";//密匙
    QByteArray data = "00068900002||20180927095839|||3002800173|0200000777|01|0155129175|||";//加密字符串
    QByteArray base64=key.fromBase64(key);
    int keynum = base64.size()/8;//密匙个数 8个一组

    unsigned char *pdata = (unsigned char*)data.data();
    unsigned char *pkey = (unsigned char*)base64.data();
    unsigned char rdata[100000]={0};
	qDebug()<<"加密数据:\n"<<data<<"\n";

    CDES mdes;
    //DES ECB 加密
    qDebug()<<"ECB ENCRYPT:"<<mdes.RunDes(CDES::ENCRYPT,CDES::ECB,pdata,rdata,data.size(),pkey,base64.size());
    QByteArray str1((char*)rdata,base64.size()*keynum);
    qDebug()<<str1.toBase64();
    qDebug()<<"ECB DECRYPT:"<<mdes.RunDes(CDES::DECRYPT,CDES::ECB,(unsigned char*)str1.data(),rdata,str1.size(),pkey,base64.size());
    QByteArray str2((char*)rdata,base64.size()*keynum);
    for(int i=0;i<8;i++) //删除结尾填充
        str2.replace(i+1,"");
    qDebug()<<QString::fromUtf8(str2);

    qDebug()<<"CBC ENCRYPT:"<<mdes.RunDes(CDES::ENCRYPT,CDES::CBC,pdata,rdata,data.size(),pkey,base64.size());
    QByteArray str3((char*)rdata,base64.size()*keynum);
    qDebug()<<str3.toBase64();
    qDebug()<<"CBC DECRYPT:"<<mdes.RunDes(CDES::DECRYPT,CDES::CBC,(unsigned char*)str3.data(),rdata,str3.size(),pkey,base64.size());
    QByteArray str4((char*)rdata,base64.size()*keynum);
    for(int i=0;i<8;i++) //删除结尾填充
        str4.replace(i+1,"");
    qDebug()<<QString::fromUtf8(str4);

    return a.exec();
}
