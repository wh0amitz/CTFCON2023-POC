# CTFCON2023-POC

This report documents a local elevation of privilege vulnerability in Active Directory Certificate Services (AD CS). The vulnerability is caused by a race condition vulnerability when Certsrv creates CRL files. Any standard user with a ManageCA ACL on the CA can publish CRL Distribution Points (CDPs) and move arbitrary files to a restricted directory (for example, C:\Windows\System32). An attacker could exploit this vulnerability to write a DLL to the C:\Windows\System32 directory or overwrite the service binary to achieve local privilege escalation.

This vulnerability has been successfully verified on the latest Windows system (as of October 24, 2023), and the system version is Windows Server 2022 Datacenter 21H2 (20348.2031).

I have shared this trick at CTFCON 2023, Please read my blog for details: [AD CS - New Ways to Abuse ManageCA Permissions](https://whoamianony.top/posts/ad-cs-new-ways-to-abuse-manageca-permissions/)
