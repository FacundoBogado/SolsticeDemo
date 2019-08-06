using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SolsticeDemo.Models;

namespace SolsticeDemo.Controllers
{
    public class ContactsController : ApiController
    {
        private ContactContext db = new ContactContext();

        // GET: api/Contacts
        public IQueryable<Contact> GetContacts()
        {
            return db.Contacts;
        }

        // GET: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contacts.Add(contact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();

            return Ok(contact);
        }

        private bool ContactExists(int id) => db.Contacts.Count(e => e.Id == id) > 0;

        //SEARCH: email
        [HttpGet, Route("api/Contacts/byEmail/{email}")]
        [ResponseType(typeof(Contact))]
        private IHttpActionResult GetByEmail(string email) => 
            ReturnValidContact(db.Contacts.FirstOrDefault(e => e.Email.Equals(email)));

        //SEARCH: phone number
        [HttpGet, Route("api/Contacts/byPhoneNumber/{phone}")]
        [ResponseType(typeof(Contact))]
        private IHttpActionResult GetByPhoneNumber(int  phone) => 
            ReturnValidContact(db.Contacts.FirstOrDefault(e => e.PhoneNumber.Equals(phone)));

        //SEARCH: all contacts by states
        [HttpGet, Route("api/Contacts/byState/{state}")]
        [ResponseType(typeof(List<Contact>))]
        private IHttpActionResult GetByState(string state) =>
            ReturnValidContact(db.Contacts.Where(e => e.Address.State.Equals(state)));

        //SEARCH: all contacts by City
        [HttpGet, Route("api/Contacts/byCity/{city}")]
        [ResponseType(typeof(List<Contact>))]
        private IHttpActionResult GetByCity(string city) =>
            ReturnValidContact(db.Contacts.Where(e => e.Address.City.Equals(city)));
        
        [ResponseType(typeof(List<Contact>))]
        private IHttpActionResult ReturnValidContact(IQueryable<Contact> contact)
        {
            if (contact == null || contact.Count() == 0)
                return NotFound();
            return Ok(contact);
        }
        
        [ResponseType(typeof(Contact))]
        private IHttpActionResult ReturnValidContact(Contact contact)
        {
            if (contact == null) 
                return NotFound();
            return Ok(contact);
        }
    }
}