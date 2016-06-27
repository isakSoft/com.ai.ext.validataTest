=======================================================
# Test cases are completed using PostMan Chrome Extension
=======================================================

**Web API calling from Postman**
> Phonebook
* URL: http://localhost:58821/api/phonebook/
*	Method: HttpGet

> GetContact
*	URL: http://localhost:58821/api/phonebook/contact/
*	Method: HttpGet
*	Params:{
		Id:d5d4433f-af32-4421-b2fb-3f771d6dbdb9
	}
*	URL(+ Params): http://localhost:58821/api/phonebook/contact/?Id=d5d4433f-af32-4421-b2fb-3f771d6dbdb9
   
> PostContact
*	URL: http://localhost:58821/api/phonebook/
*	Method: HttpPost
*	Headers: {
		Content-Type: application/json
	}
*	Body: {
		x-www-form-urlencoded: {
			ContactID:
			Firstname:Ardit
			Lastname:Isaku
			Type:Mobile
			Number:1111111
		}
	}
	
> SearchContact
*	URL: http://localhost:58821/api/phonebook/search/
*	Method: HttpGet
*	Params:{
		keyword:Ardit
	}
*	URL(+ Params): http://localhost:58821/api/phonebook/search/?keyword=Ardit
	
> DeleteContact
*	URL: http://localhost:58821/api/phonebook/delete/
*	Method: HttpPost
*	Params:{
		Id:d5d4433f-af32-4421-b2fb-3f771d6dbdb9
	}
*	URL(+ Params): http://localhost:58821/api/phonebook/delete/?Id=d5d4433f-af32-4421-b2fb-3f771d6dbdb9
	

**XML Documentation:**

Use NUGET Package Manager command: Install-Package Microsoft.AspNet.WebApi.HelpPage
