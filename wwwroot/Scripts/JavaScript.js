if (navigator.userAgentData) {
    navigator.userAgentData.getHighEntropyValues(['userAgent']).then(data => {
        const userAgent = data.userAgent;
       
    });
} else {
   
    const userAgent = navigator.userAgent;
 
}
