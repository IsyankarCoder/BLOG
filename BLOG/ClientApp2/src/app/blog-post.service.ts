import { Injectable } from '@angular/core';
import {HttpClient,HttpHeaders} from '@angular/common/http';
import {Observable,throwError} from 'rxjs';
import {retry,catchError} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {BlogPost} from '../model/blogpost';


@Injectable({
  providedIn: 'root'
})
export class BlogPostService {
  
  myAppUrl:string;
  myApiUrl:string;
  httpOptions={
    headers:new HttpHeaders({
       'Content-Type':'application/json;charset=utf-8'
    })
  };
  constructor(private http:HttpClient) {
     this.myAppUrl=environment.appUrl;
     this.myApiUrl='api/BlogPost/';
  }
 
 getBlogPosts():Observable<BlogPost[]>
 { 
    return this.http.get<BlogPost[]>(this.myAppUrl+this.myApiUrl).pipe(retry(1),catchError(this.errorHandler));
 }

 getBlogPost(postId:number):Observable<BlogPost>
 {
  return this.http.get<BlogPost>(this.myAppUrl+this.myApiUrl+postId).pipe(retry(1),catchError(this.errorHandler));
 }

 saveBlogPost(blogpost):Observable<BlogPost>
 {
 return this.http.post<BlogPost>(this.myAppUrl+this.myApiUrl,JSON.stringify(blogpost),this.httpOptions).pipe(retry(1),catchError(this.errorHandler));
 }
 
 updateBlogPost(postId:number,blogpost):Observable<BlogPost>
 {
 return this.http.put<BlogPost>(this.myAppUrl+this.myApiUrl+postId,JSON.stringify(blogpost),this.httpOptions).pipe(retry(1),catchError(this.errorHandler));
 }

 deleteBlogPost(postId:number):Observable<BlogPost>
 {
    return this.http.delete<BlogPost>(this.myAppUrl+this.myApiUrl+postId).pipe(retry(1),catchError(this.errorHandler));
 }

  
  errorHandler(error)
  {
    let errorMessage='';
    if(error.error instanceof ErrorEvent)
    {
      //Get Client-Side Error
      errorMessage =error.error.message;

    }else
    {
      //Get Server-Side Error
      errorMessage='Error Code : ${error.status}\nMessage:${error.Message}';
      
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
