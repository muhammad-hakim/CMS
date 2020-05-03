
// let compare = function(currentVersion, previousVersion)
// {
//     let currentHtml = $('.main').html();

//     $('body').append("<iframe src='http://localhost:5000/ui/en/FAQ/FAQ-06/1.9/view'></iframe>");

//     let previousHtml = $('iframe').get(0).contentDocument.querySelector('.main').outerHTML;

//     let diff = htmldiff(previousHtml, currentHtml);
    
//     $('.main').html(diff);
// }

// let originalHTML = `
// <div id="content" class="row content"><div class="col-md-12 header"><h3 class="title">Hello World</h3></div> <div class="col-md-12 header"></div> <div class="col-md-12"><p>ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du</p></div> <div class="col-md-12"><p>ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du

// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du


// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du</p></div> <!----> <div class="col-md-12"><h4>Related Contents:</h4> <table class="table table-striped table-bordered"><thead class="thead-dark"><tr><th></th> <th>Site</th> <th>Content</th></tr></thead> <tbody><tr><td>1</td> <td><a href="service" readonly="readonly">service</a></td> <td><a href="DEC-1270" readonly="readonly">DEC-1270</a></td></tr><tr><td>2</td> <td><a href="sop" readonly="readonly">sop</a></td> <td><a href="SOP-010" readonly="readonly">SOP-010</a></td></tr></tbody></table></div></div>
// `;

// let newHTML = `
// <div id="content" class="row content"><div class="col-md-12 header"><h3 class="title">Hello World this is me</h3></div> <div class="col-md-12 header"></div> <div class="col-md-12"><p>ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du</p></div> <div class="col-md-12"><p>ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du
// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du
// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du
// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du
// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du
// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du
// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du
// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du
// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du

// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du


// ake a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply du</p></div> <div class="col-md-12"><h4>Attachments:</h4> <table class="table table-striped table-bordered"><thead class="thead-dark"><tr><th></th> <th>Name</th> <th>Type</th></tr></thead> <tbody><tr><td>1</td> <td><a href="download/CSO - Job Decription.docx" readonly="readonly">CSO - Job Decription.docx</a></td> <td>SRS</td></tr><!----></tbody></table></div> <div class="col-md-12"><h4>Related Contents:</h4> <table class="table table-striped table-bordered"><thead class="thead-dark"><tr><th></th> <th>Site</th> <th>Content</th></tr></thead> <tbody><tr><td>1</td> <td><a href="service" readonly="readonly">service</a></td> <td><a href="DEC-1270" readonly="readonly">DEC-1270</a></td></tr><tr><td>2</td> <td><a href="sop" readonly="readonly">sop</a></td> <td><a href="SOP-010" readonly="readonly">SOP-010</a></td></tr></tbody></table></div></div>
// `;

// // Diff HTML strings
// let output = htmldiff(originalHTML, newHTML);

// // Show HTML diff output as HTML (crazy right?)!
// document.getElementById("output").innerHTML = output;