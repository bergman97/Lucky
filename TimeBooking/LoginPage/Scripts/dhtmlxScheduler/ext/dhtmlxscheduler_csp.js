/*
@license
dhtmlxScheduler.Net v.3.4.0 Professional Evaluation

This software is covered by DHTMLX Evaluation License. Contact sales@dhtmlx.com to get Commercial or Enterprise license. Usage without proper license is prohibited.

(c) Dinamenta, UAB.
*/
scheduler.date.date_to_str=function(e,t){return function(a){return e.replace(/%[a-zA-Z]/g,function(e){switch(e){case"%d":return t?scheduler.date.to_fixed(a.getUTCDate()):scheduler.date.to_fixed(a.getDate());case"%m":return t?scheduler.date.to_fixed(a.getUTCMonth()+1):scheduler.date.to_fixed(a.getMonth()+1);case"%j":return t?a.getUTCDate():a.getDate();case"%n":return t?a.getUTCMonth()+1:a.getMonth()+1;case"%y":return t?scheduler.date.to_fixed(a.getUTCFullYear()%100):scheduler.date.to_fixed(a.getFullYear()%100);
case"%Y":return t?a.getUTCFullYear():a.getFullYear();case"%D":return t?scheduler.locale.date.day_short[a.getUTCDay()]:scheduler.locale.date.day_short[a.getDay()];case"%l":return t?scheduler.locale.date.day_full[a.getUTCDay()]:scheduler.locale.date.day_full[a.getDay()];case"%M":return t?scheduler.locale.date.month_short[a.getUTCMonth()]:scheduler.locale.date.month_short[a.getMonth()];case"%F":return t?scheduler.locale.date.month_full[a.getUTCMonth()]:scheduler.locale.date.month_full[a.getMonth()];case"%h":
return t?scheduler.date.to_fixed((a.getUTCHours()+11)%12+1):scheduler.date.to_fixed((a.getHours()+11)%12+1);case"%g":return t?(a.getUTCHours()+11)%12+1:(a.getHours()+11)%12+1;case"%G":return t?a.getUTCHours():a.getHours();case"%H":return t?scheduler.date.to_fixed(a.getUTCHours()):scheduler.date.to_fixed(a.getHours());case"%i":return t?scheduler.date.to_fixed(a.getUTCMinutes()):scheduler.date.to_fixed(a.getMinutes());case"%a":return t?a.getUTCHours()>11?"pm":"am":a.getHours()>11?"pm":"am";case"%A":
return t?a.getUTCHours()>11?"PM":"AM":a.getHours()>11?"PM":"AM";case"%s":return t?scheduler.date.to_fixed(a.getUTCSeconds()):scheduler.date.to_fixed(a.getSeconds());case"%W":return t?scheduler.date.to_fixed(scheduler.date.getUTCISOWeek(a)):scheduler.date.to_fixed(scheduler.date.getISOWeek(a));default:return e}})}},scheduler.date.str_to_date=function(e,t){return function(a){for(var i=[0,0,1,0,0,0],n=a.match(/[a-zA-Z]+|[0-9]+/g),r=e.match(/%[a-zA-Z]/g),s=0;s<r.length;s++)switch(r[s]){case"%j":case"%d":
i[2]=n[s]||1;break;case"%n":case"%m":i[1]=(n[s]||1)-1;break;case"%y":i[0]=1*n[s]+(n[s]>50?1900:2e3);break;case"%g":case"%G":case"%h":case"%H":i[3]=n[s]||0;break;case"%i":i[4]=n[s]||0;break;case"%Y":i[0]=n[s]||0;break;case"%a":case"%A":i[3]=i[3]%12+("am"==(n[s]||"").toLowerCase()?0:12);break;case"%s":i[5]=n[s]||0;break;case"%M":i[1]=scheduler.locale.date.month_short_hash[n[s]]||0;break;case"%F":i[1]=scheduler.locale.date.month_full_hash[n[s]]||0}return t?new Date(Date.UTC(i[0],i[1],i[2],i[3],i[4],i[5])):new Date(i[0],i[1],i[2],i[3],i[4],i[5]);
}};